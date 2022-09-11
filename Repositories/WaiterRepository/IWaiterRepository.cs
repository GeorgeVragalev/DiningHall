using System.Collections.Concurrent;
using DiningHall.Models;

namespace DiningHall.Repositories.WaiterRepository;

public interface IWaiterRepository
{
    public ConcurrentBag<Waiter> GenerateWaiters();
    public Waiter GetWaiterById(int id);
    public ConcurrentBag<Waiter> GetAll();
    public Waiter GetAvailableWaiter();
}