using DiningHall.Models;

namespace DiningHall.Services.WaiterService;

public interface IWaiterService
{
    public Waiter GetAvailableWaiter(IList<Waiter> waiters);
}