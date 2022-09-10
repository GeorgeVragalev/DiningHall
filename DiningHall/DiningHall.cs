using DiningHall.Models;
using DiningHall.Repositories.FoodRepository;
using DiningHall.Repositories.TableRepository;
using DiningHall.Repositories.WaiterRepository;

namespace DiningHall.DiningHall;

public class DiningHall
{
    private readonly TableRepository _tableRepository;
    private readonly IWaiterRepository _waiterRepository;
    private readonly IFoodRepository _foodRepository;

    public IList<Table> Tables;
    public IList<Waiter> Waiters;
    public IList<Food> Menu;
    public IList<Order> Orders;

    public DiningHall(TableRepository tableRepository, IWaiterRepository waiterRepository, IFoodRepository foodRepository)
    {
        _tableRepository = tableRepository;
        _waiterRepository = waiterRepository;
        _foodRepository = foodRepository;
    }

    private void InitializeDiningHall()
    {
        Tables = _tableRepository.GenerateTables();
        Waiters = _waiterRepository.GenerateWaiters();
        Menu = _foodRepository.GenerateFood();
    }

    public void RunRestaurant()
    {
        InitializeDiningHall();

        
        
        var get = Tables.Count;
        var get2 = Waiters.Count;
        var get3 = Menu.Count;
    }
    
    
}