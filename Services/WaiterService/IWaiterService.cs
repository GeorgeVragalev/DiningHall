using System.Collections.Concurrent;
using DiningHall.Models;

namespace DiningHall.Services.WaiterService;

public interface IWaiterService
{
    public Task<Waiter> GetAvailableWaiter();
    public ConcurrentBag<Waiter> GenerateWaiters();
    public Task<Waiter> GetWaiterById(int? id);
    public Task<Order> TakeOrder(Table table, Waiter waiter);
    public Task<int> FinishOrder(FinishedOrder order, Waiter waiter);
}