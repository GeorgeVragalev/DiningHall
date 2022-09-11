using System.Text;
using DiningHall.Models;
using DiningHall.Repositories.FoodRepository;
using DiningHall.Repositories.OrderRepository;
using DiningHall.Repositories.WaiterRepository;
using DiningHall.Services;
using Newtonsoft.Json;

namespace DiningHall.DiningHall;

public class DiningHall : IDiningHall
{
    private readonly IWaiterRepository _waiterRepository;
    private readonly IFoodRepository _foodRepository;
    private readonly ITableService _tableService;
    private readonly IOrderService _orderService;

    public IList<Table> Tables;
    public IList<Waiter> Waiters;
    public IList<Food> Menu;


    public DiningHall( IWaiterRepository waiterRepository, IFoodRepository foodRepository, IOrderService orderService, ITableService tableService)
    {

        _waiterRepository = waiterRepository;
        _foodRepository = foodRepository;
        _orderService = orderService;
        _tableService = tableService;
    }

    private void InitializeDiningHall()
    {
        Waiters = _waiterRepository.GenerateWaiters();
        Tables = _tableService.GenerateTables();
        Menu = _foodRepository.GenerateFood();
    }

    public async void RunRestaurant()
    {
        InitializeDiningHall();
        
        // while (true)
        // {
            Thread.Sleep(4000);
        
            var freeTableId = _tableService.GetFreeTableId();
            // var waiter = _orderService.GetAvailableWaiter(Waiters);
            if (freeTableId == 0 /*&& waiter==null*/)
            {
                Thread.Sleep(2000);
                // continue;
            }
            
            var order = _orderService.TakeOrder(freeTableId, 2);
            
            _orderService.SendOrder(order);
        // }
    }

    public async void ServeOrder(FinishedOrder finishedOrder)
    {
        var waiter = _waiterRepository.GetWaiterById(finishedOrder.WaiterId);
        
    }
    
    
}