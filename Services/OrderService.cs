using DiningHall.Models;
using DiningHall.Repositories.OrderRepository;

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

    public void SendOrder(Order order)
    {
        
    }

    public int GetFreeTable(IList<Table> tables)
    {
        foreach (var table in tables)
        {
            if (table.Status == Status.Available)
            {
                table.Status = Status.Waiting;
                return table.Id;
            }
        }

        return 0;
    }

    public Waiter GetAvailableWaiter(IList<Waiter> waiters)
    {
        foreach (var waiter in waiters)
        {
            if (!waiter.IsBusy)
            {
                return waiter;
            }
        }

        return null;
    }
}