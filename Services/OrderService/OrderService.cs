using System.Text;
using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Models.Enum;
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

    public async Task SendOrder(Order order, string url)
    {
        try
        {
            var json = JsonConvert.SerializeObject(order);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            await client.PostAsync(url, data);
            PrintConsole.Write($"Order {order.Id} with {order.Foods.Count} foods sent to kitchen from {order.OrderType}", ConsoleColor.Green);
        }
        catch (Exception e)
        {
            PrintConsole.Write($"Failed to send order id: {order.Id}",
                ConsoleColor.DarkRed);
        }
    }

    public async Task<Order> GenerateOrder(Table table, Waiter waiter)
    {
        var foodList = await _foodService.GenerateOrderFood();
        var order = new Order()
        {
            Id = IdGenerator.GenerateId(),
            Priority = RandomGenerator.NumberGenerator(5),
            PickUpTime = DateTime.Now,
            Foods = foodList,
            TableId = table.Id,
            WaiterId = waiter.Id,
            MaxWait = foodList.CalculateMaxWaitingTime(_foodService),
            ClientId = 0,
            OrderType = OrderType.DiningHallOrder,
            RestaurantId = Settings.Settings.RestaurantId,
            GroupOrderId = 0
        };
        PrintConsole.Write($"Generated order: {order.Id} waiting time {order.MaxWait}", ConsoleColor.Cyan);

        return await Task.FromResult(order);
    }
}