using DiningHall.DiningHall;
using DiningHall.Models;

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

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Initialization.Start();

        await Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}