using DiningHall.Models;

namespace DiningHall.Services.WaiterService;

public class WaiterService : IWaiterService
{
    
    
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