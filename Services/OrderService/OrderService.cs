using System.Text;
using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Services.FoodService;
using Newtonsoft.Json;

namespace DiningHall.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly IFoodService _foodService;

    public OrderService(IFoodService foodService)
    {
        _foodService = foodService;
    }
    
    public async Task SendOrder(Order order)
    {
        try
        {
            var json = JsonConvert.SerializeObject(order);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = Settings.Settings.KitchenUrl;
            using var client = new HttpClient();

            await client.PostAsync(url, data);
            PrintConsole.Write("Order "+ order.Id+" sent to kitchen", ConsoleColor.Green);
        }
        catch (Exception e)
        {
            PrintConsole.Write(Thread.CurrentThread.Name + " Failed to send order id: "+ order.Id, ConsoleColor.DarkRed);
        }
    }
public async Task<Order> GenerateOrder(Table table, Waiter waiter)
    {
        var foodList = await _foodService.GenerateOrderFood();
        var order = new Order
        {
            Id = IdGenerator.GenerateId(),
            Priority = RandomGenerator.NumberGenerator(3),
            PickUpTime = DateTime.UtcNow,
            Foods = foodList,
            TableId = table.Id,
            WaiterId = waiter.Id,
            MaxWait = foodList.CalculateMaxWaitingTime(_foodService)
        };
        PrintConsole.Write("Generated order: "+ order.Id, ConsoleColor.Cyan);

        return await Task.FromResult(order);
    }
    
}