using System.Collections.Concurrent;
using System.Diagnostics;
using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Repositories.FoodRepository;
using DiningHall.Services.FoodService;
using DiningHall.Services.OrderService;
using DiningHall.Services.TableService;
using DiningHall.Services.WaiterService;
using Console = System.Console;

namespace DiningHall.DiningHall;

public class DiningHall : IDiningHall
{
    private readonly IWaiterService _waiterService;
    private readonly ITableService _tableService;
    private readonly IOrderService _orderService;
    private readonly IFoodService _foodService;
    private static Mutex _mutex = new();

    private decimal _rating = 5;

    public ConcurrentBag<Table> Tables;
    public ConcurrentBag<Waiter> Waiters;
    public ConcurrentBag<Food> Menu;

    public DiningHall(IOrderService orderService, ITableService tableService,
        IWaiterService waiterService, IFoodService foodService)
    {
        _orderService = orderService;
        _tableService = tableService;
        _waiterService = waiterService;
        _foodService = foodService;
    }

    private void InitializeDiningHall()
    {
        Waiters = _waiterService.GenerateWaiters();
        Tables = _tableService.GenerateTables();
        Menu = _foodService.GenerateFood();
    }

    public void ExecuteCode(CancellationToken cancellationToken)
    {
        InitializeDiningHall();
        
        //Initialize threads to continue with running the method
        RunThreads(cancellationToken);
    }

    private async void RunThreads(CancellationToken cancellationToken)
    {
        Thread t1 = new Thread(() => RunRestaurant(cancellationToken));
        Thread t2 = new Thread(() => RunRestaurant(cancellationToken));
        Thread t3 = new Thread(() => RunRestaurant(cancellationToken));
        Thread t4 = new Thread(() => RunRestaurant(cancellationToken));
        Thread t5 = new Thread(() => RunRestaurant(cancellationToken));
        t1.Start();
        t2.Start();
        t3.Start();
        t4.Start();
        t5.Start();
    }

    public async void RunRestaurant(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            _mutex.WaitOne();
            var freeTable = await _tableService.GetFreeTable();
            var waiter = await _waiterService.GetAvailableWaiter();
           
            if (freeTable != null && waiter != null)
            {
                Console.WriteLine();
                PrintConsole.Write(Thread.CurrentThread.Name + " got table: "+ freeTable.Id, ConsoleColor.DarkBlue);
                PrintConsole.Write(Thread.CurrentThread.Name + " got waiter: "+ waiter.Id, ConsoleColor.DarkBlue);
                
                //change status of waiter and table here
                freeTable.Status = Status.Waiting;
                waiter.IsBusy = true;
                _mutex.ReleaseMutex();
                var order = await _waiterService.TakeOrder(freeTable, waiter);
                waiter.ActiveOrdersIds.Add(order.Id);

                await _orderService.SendOrder(order);

                //free up waiter after sending request to kitchen
                waiter.IsBusy = false;
            }
            else
            {
                _mutex.ReleaseMutex();
            }
            //if there are no free tables or waiters, wait and go to next iteration
            //todo random thread sleep
            Thread.Sleep(5000);
        }
    }

    //constant/variable sec/min/mlsec

    //unixtimestamp 1 sec = 1 unix
    //time units * 0.1 = ms  , *1 = sec , *60 = min

    public async void ServeOrder(FinishedOrder finishedOrder)
    {
        _mutex.WaitOne();
        var waiter = await _waiterService.GetWaiterById(finishedOrder.WaiterId);
        var table = await _tableService.GetTableById(finishedOrder.TableId);
        if (table != null && waiter != null)
        {
            waiter.IsBusy = true;
            table.Status = Status.ReceivedOrder;
            PrintConsole.Write("Serving order got waiter: "+ waiter.Id, ConsoleColor.DarkGreen);
            _mutex.ReleaseMutex();

            await _waiterService.FinishOrder(finishedOrder, waiter);

            var waitingTime = finishedOrder.GetOrderRating();
            _rating = (_rating + waitingTime) / 2;
            Console.WriteLine("Current rating: " + _rating);
            waiter.IsBusy = false;

            Thread.Sleep(2000);
            table.Status = Status.Available;
        }
        else
        {
            Console.WriteLine("Failed to serve order with id: " + finishedOrder.Id);
        }
    }
}