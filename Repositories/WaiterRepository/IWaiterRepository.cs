using DiningHall.Models;

namespace DiningHall.Repositories.WaiterRepository;

public interface IWaiterRepository
{
    public IList<Waiter> GenerateWaiters();
    public Waiter GetWaiterById(int id);
    public IList<Waiter> GetAll();
    public Waiter GetAvailableWaiter();
}