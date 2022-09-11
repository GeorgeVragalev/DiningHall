using System.Collections.Concurrent;
using DiningHall.Models;
using DiningHall.Repositories.WaiterRepository;
using DiningHall.Services.OrderService;

namespace DiningHall.Services.WaiterService;

public class WaiterService : IWaiterService
{
    private readonly IWaiterRepository _waiterRepository;
    private readonly IOrderService _orderService;

    public WaiterService(IWaiterRepository waiterRepository, IOrderService orderService)
    {
        _waiterRepository = waiterRepository;
        _orderService = orderService;
    }

    public Waiter GetAvailableWaiter()
    {
        var waiter = _waiterRepository.GetAvailableWaiter();
        
        return waiter;
    }

    public ConcurrentBag<Waiter> GenerateWaiters()
    {
        return _waiterRepository.GenerateWaiters();
    }

    public Waiter GetWaiterById(int id)
    {
        return _waiterRepository.GetWaiterById(id);
    }

    public Order TakeOrder(int tableId, int waiterId)
    {
        return _orderService.GenerateOrder(tableId, waiterId);
    }

    public void ServeOrder(FinishedOrder order, Waiter waiter)
    {
        
    }
}