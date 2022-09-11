using DiningHall.Models;

namespace DiningHall.Repositories.WaiterRepository;

public class WaiterRepository : IWaiterRepository
{
    private readonly IList<Waiter> _waiters = new List<Waiter>();

    public IList<Waiter> GenerateWaiters()
    {
        var nrOfWaiters = Settings.Settings.Waiters;
        for (var id = 1; id <= nrOfWaiters; id++)
        {
            _waiters.Add(new Waiter
            {
                Id = id,
                Name = $"Waiter {id}",
                IsBusy = false
            });
        }

        return _waiters;
    }

    public Waiter GetWaiterById(int id)
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

        return waiterResult;
    }

    public IList<Waiter> GetAll()
    {
        return _waiters;
    }

    public Waiter GetAvailableWaiter()
    {
        foreach (var waiter in _waiters)
        {
            if (!waiter.IsBusy)
            {
                return waiter;
            }
        }
        
        return null;
    }
}