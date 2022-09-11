using DiningHall.Models;

namespace DiningHall.Services.OrderService;

public interface IOrderService
{
    public void SendOrder(Order order);
    public Order GenerateOrder(int table, int waiter);
}