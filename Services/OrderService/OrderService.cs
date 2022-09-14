using System.Text;
using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Repositories.FoodRepository;
using DiningHall.Services.FoodService;
using Newtonsoft.Json;

namespace DiningHall.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly IFoodService _foodService;
    private readonly ILogger<OrderService> _logger;

    public OrderService(ILogger<OrderService> logger, IFoodService foodService)
    {
        _logger = logger;
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

            var response = await client.PostAsync(url, data);
            Console.WriteLine("Order "+ order.Id+" sent to kitchen");
            var result = await response.Content.ReadAsStringAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to send order with id:" + order.Id);
        }
    }
public async Task<Order> GenerateOrder(Table table, Waiter waiter)
    {
        var foodList = await _foodService.GenerateOrderFood();
        return await Task.FromResult(new Order
        {
            Id = IdGenerator.GenerateId(),
            Priority = RandomGenerator.NumberGenerator(3),
            PickUpTime = DateTime.UtcNow,
            Foods = foodList,
            TableId = table.Id,
            WaiterId = waiter.Id,
            MaxWait = foodList.CalculateMaxWaitingTime(_foodService)
        });
    }
    
}