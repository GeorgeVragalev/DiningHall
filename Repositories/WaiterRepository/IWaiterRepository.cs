using System.Collections.Concurrent;
using DiningHall.Models;

namespace DiningHall.Repositories.WaiterRepository;

public interface IWaiterRepository
{
    public ConcurrentBag<Waiter> GenerateWaiters();
    public Task<Waiter> GetById(int id);
    public ConcurrentBag<Waiter> GetAll();
    public Task<Waiter> GetAvailableWaiter();
}