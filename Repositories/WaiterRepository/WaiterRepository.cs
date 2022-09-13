using System.Collections.Concurrent;
using DiningHall.Models;
using DiningHall.Repositories.Generic;

namespace DiningHall.Repositories.WaiterRepository;

public class WaiterRepository : IWaiterRepository
{
    private readonly ConcurrentBag<Waiter> _waiters = new ConcurrentBag<Waiter>();

    public ConcurrentBag<Waiter> GenerateWaiters()
    {
        var nrOfWaiters = Settings.Settings.Waiters;
        for (var id = 1; id <= nrOfWaiters; id++)
        {
            _waiters.Add(new Waiter
            {
                Id = id,
                Name = $"Waiter {id}",
                IsBusy = false,
                ActiveOrdersIds = new List<int>(),
                CompletedOrderIds = new List<int>()
            });
        }

        return _waiters;
    }

    public ConcurrentBag<Waiter> GetAll()
    {
        return _waiters;
    }

    public async Task<Waiter> GetAvailableWaiter()
    {
        foreach (var waiter in _waiters)
        {
            if (!waiter.IsBusy)
            {
                return await Task.FromResult(waiter);
            }
        }
        
        return null!;
    }

    public async Task<Waiter> GetById(int id)
    {
        var waiterResult = new Waiter();
        foreach (var waiter in _waiters)
        {
            if (waiter.Id == id)
            {
                waiterResult = waiter;
                break;
            }
        }

        return await Task.FromResult(waiterResult);
        
    }
}