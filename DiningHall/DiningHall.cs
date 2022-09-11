using System.Text;
using DiningHall.Models;
using DiningHall.Repositories.FoodRepository;
using DiningHall.Repositories.OrderRepository;
using DiningHall.Repositories.WaiterRepository;
using DiningHall.Services;
using DiningHall.Services.OrderService;
using DiningHall.Services.TableService;
using DiningHall.Services.WaiterService;
using Newtonsoft.Json;

namespace DiningHall.DiningHall;

public class DiningHall : IDiningHall
{
    private readonly IWaiterService _waiterService;
    private readonly IFoodRepository _foodRepository;
    private readonly ITableService _tableService;
    private readonly IOrderService _orderService;

    public IList<Table> Tables;
    public IList<Waiter> Waiters;
    public IList<Food> Menu;


    public DiningHall(IFoodRepository foodRepository, IOrderService orderService, ITableService tableService, IWaiterService waiterService)
    {

        _foodRepository = foodRepository;
        _orderService = orderService;
        _tableService = tableService;
        _waiterService = waiterService;
    }

    private void InitializeDiningHall()
    {
        Waiters = _waiterService.GenerateWaiters();
        Tables = _tableService.GenerateTables();
        Menu = _foodRepository.GenerateFood();
    }

    public async void RunRestaurant()
    {
        InitializeDiningHall();
        
        while (true)
        {
            Thread.Sleep(4000);
        
            var freeTableId = _tableService.GetFreeTableId();
            var waiter = _waiterService.GetAvailableWaiter();
            if (freeTableId == 0 && waiter==null)
            {
                Thread.Sleep(2000);
                // continue;
            }
            
            var order = _orderService.TakeOrder(freeTableId, waiter.Id);
            
            _orderService.SendOrder(order);
        }
    }

    public async void ServeOrder(FinishedOrder finishedOrder)
    {
        var waiter = _waiterService.GetWaiterById(finishedOrder.WaiterId);
        
    }
    
    
}