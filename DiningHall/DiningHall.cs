using System.Collections.Concurrent;
using System.Diagnostics;
using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Repositories.FoodRepository;
using DiningHall.Services.FoodService;
using DiningHall.Services.OrderService;
using DiningHall.Services.TableService;
using DiningHall.Services.WaiterService;

namespace DiningHall.DiningHall;

public class DiningHall : IDiningHall
{
    private readonly IWaiterService _waiterService;
    private readonly ITableService _tableService;
    private readonly IOrderService _orderService;
    private readonly IFoodService _foodService;
    private readonly ILogger<DiningHall> _logger;

    private decimal _rating = 5;

    public ConcurrentBag<Table> Tables;
    public ConcurrentBag<Waiter> Waiters;
    public ConcurrentBag<Food> Menu;

    public DiningHall(IOrderService orderService, ITableService tableService,
        IWaiterService waiterService, ILogger<DiningHall> logger, IFoodService foodService)
    {
        _orderService = orderService;
        _tableService = tableService;
        _waiterService = waiterService;
        _logger = logger;
        _foodService = foodService;
    }

    private void InitializeDiningHall()
    {
        Waiters = _waiterService.GenerateWaiters();
        Tables = _tableService.GenerateTables();
        Menu = _foodService.GenerateFood();
    }

    public async void RunRestaurant(CancellationToken cancellationToken)
    {
        InitializeDiningHall();

        while (!cancellationToken.IsCancellationRequested)
        {
            var freeTable = await _tableService.GetFreeTable();
            var waiter = await _waiterService.GetAvailableWaiter();
            if (freeTable != null && waiter != null)
            {
                //change status of waiter and table here
                freeTable.Status = Status.Waiting;
                waiter.IsBusy = true;
                var order = await _waiterService.TakeOrder(freeTable, waiter);
                waiter.ActiveOrdersIds.Add(order.Id);
        
                await _orderService.SendOrder(order);
                
                //free up waiter after sending request to kitchen
                waiter.IsBusy = false;
            }
            //if there are no free tables or waiters, wait and go to next iteration
            //todo random thread sleep
            Thread.Sleep(2000);
        }
    }
    
    //constant/variable sec/min/mlsec
    
    //unixtimestamp 1 sec = 1 unix
    //time units * 0.1 = ms  , *1 = sec , *60 = min

    public async void ServeOrder(FinishedOrder finishedOrder)
    {
        var waiter = await _waiterService.GetWaiterById(finishedOrder.WaiterId);
        var table = await _tableService.GetTableById(finishedOrder.TableId);
        waiter.IsBusy = true;
        table.Status = Status.ReceivedOrder;
        await _waiterService.FinishOrder(finishedOrder, waiter);
        
        var waitingTime = finishedOrder.GetOrderRating();
        _rating = (_rating + waitingTime) / 2;
        _logger.LogInformation("Current rating: "+ _rating);
        waiter.IsBusy = false;
        
        Thread.Sleep(2000);
        table.Status = Status.Available;
    }
}