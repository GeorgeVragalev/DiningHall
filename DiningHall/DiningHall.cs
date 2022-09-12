using System.Collections.Concurrent;
using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Repositories.FoodRepository;
using DiningHall.Services.OrderService;
using DiningHall.Services.TableService;
using DiningHall.Services.WaiterService;

namespace DiningHall.DiningHall;

public class DiningHall : IDiningHall
{
    private readonly IWaiterService _waiterService;
    private readonly IFoodRepository _foodRepository;
    private readonly ITableService _tableService;
    private readonly IOrderService _orderService;

    public ConcurrentBag<Table> Tables;
    public ConcurrentBag<Waiter> Waiters;
    public ConcurrentBag<Food> Menu;

    public DiningHall(IFoodRepository foodRepository, IOrderService orderService, ITableService tableService,
        IWaiterService waiterService)
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

    public void RunRestaurant(CancellationToken cancellationToken)
    {
        InitializeDiningHall();

        while (!cancellationToken.IsCancellationRequested)
        {
            Thread.Sleep(5000);
        
            var freeTableId = _tableService.GetFreeTableId();
            var waiter = _waiterService.GetAvailableWaiter();
            if (freeTableId == 0 && waiter == null)
            {
                continue;
            }
        
            var order = _waiterService.TakeOrder(freeTableId, waiter.Id);
        
            _orderService.SendOrder(order);
        }
    }

    public async void ServeOrder(FinishedOrder finishedOrder)
    {
        var waiter = _waiterService.GetWaiterById(finishedOrder.WaiterId);
        // var table = _tableService
        _waiterService.ServeOrder(finishedOrder, waiter);
    }
}