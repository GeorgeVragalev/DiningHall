using System.Collections.Concurrent;
using DiningHall.Models;

namespace DiningHall.Services.WaiterService;

public interface IWaiterService
{
    public Waiter GetAvailableWaiter();
    public ConcurrentBag<Waiter> GenerateWaiters();
    public Waiter GetWaiterById(int id);
    public Order TakeOrder(int tableId, int waiterId);
    public void ServeOrder(FinishedOrder order, Waiter waiter);
}