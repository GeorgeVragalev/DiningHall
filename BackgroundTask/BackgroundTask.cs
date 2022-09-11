using DiningHall.DiningHall;

namespace DiningHall.BackgroundTask;

public class BackgroundTask : BackgroundService
{
    private readonly ILogger<BackgroundTask> logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private Timer timer;
    private int number;

    public BackgroundTask(ILogger<BackgroundTask> logger, IServiceScopeFactory serviceScopeFactory)
    {
        this.logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public void Dispose()
    {
        timer?.Dispose();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var scoped = scope.ServiceProvider.GetRequiredService<IDiningHall>();
            Thread.Sleep(4000);

            scoped.RunRestaurant();
            //Do your stuff
        }

        await Task.CompletedTask;
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