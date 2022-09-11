using System.Text;
using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Repositories.FoodRepository;
using Newtonsoft.Json;

namespace DiningHall.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly IFoodRepository _foodRepository;
    private readonly ILogger<OrderService> _logger;

    public OrderService(IFoodRepository foodRepository, ILogger<OrderService> logger)
    {
        _foodRepository = foodRepository;
        _logger = logger;
    }


    public async void SendOrder(Order order)
    {
        try
        {
            var json = JsonConvert.SerializeObject(order);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = Settings.Settings.KitchenUrl;
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);
            _logger.LogInformation("Order "+ order.Id+" sent to kitchen");
            var result = await response.Content.ReadAsStringAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to send order with id:" + order.Id);
        }
    }

    public Order GenerateOrder(int table, int waiter)
    {
        var foodList = _foodRepository.GenerateOrderFood();
        return new Order
        {
            Id = IdGenerator.GenerateId(),
            Priority = RandomGenerator.NumberGenerator(3),
            PickUpTime = DateTime.UtcNow,
            Foods = foodList,
            TableId = table,
            WaiterId = waiter,
            MaxWait = foodList.CalculateMaxWaitingTime(_foodRepository)
        };
    }
}