using DiningHall.Models;

namespace DiningHall.Services;

public interface IOrderService
{
    public Order TakeOrder(int tableId, int waiterId);
    public void SendOrder(Order order);
}