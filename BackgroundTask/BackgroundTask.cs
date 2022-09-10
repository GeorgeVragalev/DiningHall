namespace DiningHall.BackgroundTask;

public class BackgroundTask : IHostedService, IDisposable
{
    private readonly ILogger<BackgroundTask> logger;
    private Timer timer;
    private int number;

    public BackgroundTask(ILogger<BackgroundTask> logger)
    {
        this.logger = logger;
    }

    public void Dispose()
    {
        timer?.Dispose();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        timer = new Timer(o =>
            {
                Interlocked.Increment(ref number);
                logger.LogInformation($"Printing the worker number {number}");
            },
            null,
            TimeSpan.Zero,
            TimeSpan.FromSeconds(5));

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}