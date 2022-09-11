using System.Text;
using DiningHall.Models;
using DiningHall.Repositories.OrderRepository;
using Newtonsoft.Json;

namespace DiningHall.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;


    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Order TakeOrder(int tableId, int waiterId)
    {
        return _orderRepository.GenerateOrder(tableId, waiterId);
    }

    public async void SendOrder(Order order)
    {
        var json = JsonConvert.SerializeObject(order);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var url = "https://localhost:7090/api/Order";
        using var client = new HttpClient();

        var response = await client.PostAsync(url, data);

        var result = await response.Content.ReadAsStringAsync();
    }

  
}