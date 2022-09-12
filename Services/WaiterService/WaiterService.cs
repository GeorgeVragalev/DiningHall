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

    public async Task<Waiter> GetAvailableWaiter()
    {
        var waiter = await _waiterRepository.GetAvailableWaiter();
        
        return waiter;
    }

    public ConcurrentBag<Waiter> GenerateWaiters()
    {
        return _waiterRepository.GenerateWaiters();
    }

    public async Task<Waiter> GetWaiterById(int id)
    {
        return await _waiterRepository.GetWaiterById(id);
    }

    public async Task<Order> TakeOrder(Table table, Waiter waiter)
    {
        return await _orderService.GenerateOrder(table, waiter);
    }

    public Task<int> ServeOrder(FinishedOrder order, Waiter waiter)
    {
        int waitingTime = order.MaxWait - order.CookingTime;
        waiter.CompletedOrderIds.Add(order.Id);
        return Task.FromResult(waitingTime);
    }
}