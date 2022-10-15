using DiningHall.BackgroundTask;
using DiningHall.DiningHall;
using DiningHall.Models;
using DiningHall.Repositories.FoodRepository;
using DiningHall.Repositories.Generic;
using DiningHall.Repositories.TableRepository;
using DiningHall.Repositories.WaiterRepository;
using DiningHall.Services;
using DiningHall.Services.FoodService;
using DiningHall.Services.OrderService;
using DiningHall.Services.RestaurantService;
using DiningHall.Services.TableService;
using DiningHall.Services.WaiterService;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace DiningHall.Settings;
public class Startup
{
    private IConfiguration ConfigRoot { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddLogging(config => config.ClearProviders());
        
        services.AddSingleton<IWaiterRepository, WaiterRepository>();
        services.AddSingleton<ITableRepository, TableRepository>();
        services.AddSingleton<IFoodRepository, FoodRepository>();
        services.AddSingleton<ITableService, TableService>();
        services.AddSingleton<IWaiterService, WaiterService>();
        services.AddSingleton<IOrderService, OrderService>();
        services.AddSingleton<IFoodService, FoodService>();
        services.AddSingleton<IRestaurantService, RestaurantService>();
        services.AddSingleton<IDiningHall, DiningHall.DiningHall>();
        services.AddHostedService<BackgroundTask.BackgroundTask>();
    }

    public Startup(IConfiguration configuration)
    {
        ConfigRoot = configuration;
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseHsts();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.Run();
    }
}