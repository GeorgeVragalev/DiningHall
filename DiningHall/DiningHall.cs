using DiningHall.Models;
using DiningHall.Repositories.FoodRepository;
using DiningHall.Repositories.TableRepository;
using DiningHall.Repositories.WaiterRepository;
using DiningHall.Services;

namespace DiningHall.DiningHall;

public class DiningHall : IDiningHall
{
    private readonly TableRepository _tableRepository;
    private readonly IWaiterRepository _waiterRepository;
    private readonly IFoodRepository _foodRepository;
    private readonly IOrderService _orderService;

    public IList<Table> Tables;
    public IList<Waiter> Waiters;
    public IList<Food> Menu;


    public DiningHall(TableRepository tableRepository, IWaiterRepository waiterRepository, IFoodRepository foodRepository, IOrderService orderService)
    {
        _tableRepository = tableRepository;
        _waiterRepository = waiterRepository;
        _foodRepository = foodRepository;
        _orderService = orderService;
    }

    private void InitializeDiningHall()
    {
        Tables = _tableRepository.GenerateTables();
        Waiters = _waiterRepository.GenerateWaiters();
        _foodRepository.GenerateFood();
    }

    public void RunRestaurant()
    {
        InitializeDiningHall();

        while (true)
        {
            var freeTableId = _orderService.GetFreeTable(Tables);
            var waiter = _orderService.GetAvailableWaiter(Waiters);
            if (freeTableId == 0 && waiter==null)
            {
                Thread.Sleep(2000);
                continue;
            }
            
            var order = _orderService.TakeOrder(freeTableId, waiter.Id);
            
            _orderService.SendOrder(order);
        }
    }
    
    
}