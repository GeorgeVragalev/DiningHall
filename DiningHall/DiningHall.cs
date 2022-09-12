using System.Collections.Concurrent;
using System.Diagnostics;
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
            Thread.Sleep(2000);
        }
    }

    public async void ServeOrder(FinishedOrder finishedOrder)
    {
        var waiter = await _waiterService.GetWaiterById(finishedOrder.WaiterId);
        var table = await _tableService.GetTableById(finishedOrder.TableId);
        waiter.IsBusy = true;
        // var table = _tableService
        _waiterService.ServeOrder(finishedOrder, waiter);
        table.Status = Status.ReceivedOrder;
    }
}