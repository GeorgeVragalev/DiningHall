using DiningHall.Models;
using DiningHall.Repositories.FoodRepository;
using DiningHall.Repositories.OrderRepository;
using DiningHall.Repositories.TableRepository;
using DiningHall.Repositories.WaiterRepository;
using DiningHall.Services;

namespace DiningHall.DiningHall;

public class DiningHall : IDiningHall
{
    private readonly IWaiterRepository _waiterRepository;
    private readonly IFoodRepository _foodRepository;
    private readonly ITableRepository _tableRepository;
    private readonly IOrderService _orderService;

    public IList<Table> Tables;
    public IList<Waiter> Waiters;
    public IList<Food> Menu;


    public DiningHall( IWaiterRepository waiterRepository, IFoodRepository foodRepository, ITableRepository tableRepository, IOrderService orderService)
    {

        _waiterRepository = waiterRepository;
        _foodRepository = foodRepository;
        _tableRepository = tableRepository;
        _orderService = orderService;
    }

    private void InitializeDiningHall()
    {
   
        Waiters = _waiterRepository.GenerateWaiters();
        Tables = _tableRepository.GenerateTables();
        _foodRepository.GenerateFood();
    }

    public void RunRestaurant()
    {
        InitializeDiningHall();

        var table = _orderService.GetFreeTable(Tables);
        var waiter = _orderService.GetAvailableWaiter(Waiters);
        var order = _orderService.TakeOrder(table, waiter.Id);

        var a = 2;
    }
    
    
}