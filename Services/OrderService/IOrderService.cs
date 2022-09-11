using DiningHall.Models;

namespace DiningHall.Services.OrderService;

public interface IOrderService
{
    public Order TakeOrder(int tableId, int waiterId);
    public void SendOrder(Order order);
}