using DiningHall.BackgroundTask;
using DiningHall.DiningHall;
using DiningHall.Models;
using DiningHall.Repositories.Context;
using DiningHall.Repositories.FoodRepository;
using DiningHall.Repositories.Generic;
using DiningHall.Repositories.OrderRepository;
using DiningHall.Repositories.TableRepository;
using DiningHall.Repositories.WaiterRepository;
using DiningHall.Services;
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
        services.AddScoped<IWaiterRepository, WaiterRepository>();
        services.AddScoped<ITableRepository, TableRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IFoodRepository, FoodRepository>();
        services.AddScoped<IGenericRepository<BaseEntity>, GenericRepository<BaseEntity>>();
        services.AddScoped<ITableService, TableService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IDiningHall, DiningHall.DiningHall>();
        services.AddHostedService<BackgroundTask.BackgroundTask>();
        services.AddDbContext<AppDbContext>();

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