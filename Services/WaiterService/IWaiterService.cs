using DiningHall.Models;

namespace DiningHall.Services.WaiterService;

public interface IWaiterService
{
    public Waiter GetAvailableWaiter();
    public IList<Waiter> GenerateWaiters();
    public Waiter GetWaiterById(int id);
}