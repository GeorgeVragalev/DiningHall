using DiningHall.Models;
using DiningHall.Repositories.WaiterRepository;

namespace DiningHall.Services.WaiterService;

public class WaiterService : IWaiterService
{
    private readonly IWaiterRepository _waiterRepository;

    public WaiterService(IWaiterRepository waiterRepository)
    {
        _waiterRepository = waiterRepository;
    }

    public Waiter GetAvailableWaiter()
    {
        var waiter = _waiterRepository.GetAvailableWaiter();

        return waiter;
    }

    public IList<Waiter> GenerateWaiters()
    {
        return _waiterRepository.GenerateWaiters();
    }

    public Waiter GetWaiterById(int id)
    {
        return _waiterRepository.GetWaiterById(id);
    }
}