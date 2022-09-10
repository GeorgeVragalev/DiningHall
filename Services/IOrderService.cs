using DiningHall.Models;

namespace DiningHall.Services;

public interface IOrderService
{
    public int GetFreeTable(IList<Table> tables);
    public Waiter GetAvailableWaiter(IList<Waiter> waiters);
    public Order TakeOrder(int tableId, int waiterId);
    public void SendOrder(Order order);
}