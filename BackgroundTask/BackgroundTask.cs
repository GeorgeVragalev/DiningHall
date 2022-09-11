using DiningHall.DiningHall;

namespace DiningHall.BackgroundTask;

public class BackgroundTask : BackgroundService
{
    private readonly ILogger<BackgroundTask> logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly Timer _timer;
    private int number;

    public BackgroundTask(ILogger<BackgroundTask> logger, IServiceScopeFactory serviceScopeFactory)
    {
        this.logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var scoped = scope.ServiceProvider.GetRequiredService<IDiningHall>();
                scoped.RunRestaurant(stoppingToken);
                await Task.Delay(500);
            }
        }
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}