using DiningHall.Models;

namespace DiningHall.Services.OrderService;

public interface IOrderService
{
    public Task SendOrder(Order order, string url);
    public Task<Order> GenerateOrder(Table table, Waiter waiter);
}