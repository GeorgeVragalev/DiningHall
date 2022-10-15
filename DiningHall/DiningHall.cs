using System.Collections.Concurrent;
using System.Diagnostics;
using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Repositories.FoodRepository;
using DiningHall.Services.FoodService;
using DiningHall.Services.OrderService;
using DiningHall.Services.RestaurantService;
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
    private readonly IRestaurantService _restaurantService;
    private static Mutex _mutex = new();

    private double _rating = 5;

    public ConcurrentBag<Table> Tables;
    public ConcurrentBag<Waiter> Waiters;
    public IList<Food> Menu;

    public DiningHall(IOrderService orderService, ITableService tableService,
        IWaiterService waiterService, IFoodService foodService, IRestaurantService restaurantService)
    {
        _orderService = orderService;
        _tableService = tableService;
        _waiterService = waiterService;
        _foodService = foodService;
        _restaurantService = restaurantService;
    }

    private void InitializeDiningHall()
    {
        Waiters = _waiterService.GenerateWaiters();
        Tables = _tableService.GenerateTables();
        Menu = _foodService.GenerateFood().ToList();
    }

    public void ExecuteCode(CancellationToken cancellationToken)
    {
        InitializeDiningHall();
        _restaurantService.RegisterRestaurant(Menu, _rating);
        RunThreads(cancellationToken);
    }

    private async void RunThreads(CancellationToken cancellationToken)
    {
        Thread t1 = new Thread(() => RunRestaurant(cancellationToken));
        Thread t2 = new Thread(() => RunRestaurant(cancellationToken));
        t1.Start();
        t2.Start();
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
                
                freeTable.Status = Status.Waiting;
                waiter.IsBusy = true;
                _mutex.ReleaseMutex();
                var order = await _waiterService.TakeOrder(freeTable, waiter);
                waiter.ActiveOrdersIds.Add(order.Id);

                await _orderService.SendOrder(order);

                waiter.IsBusy = false;
            }
            else
            {
                _mutex.ReleaseMutex();
            }
            Thread.Sleep(5000);
        }
    }

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
            _rating = waitingTime;
            PrintConsole.Write($"Current rating: {_rating}", ConsoleColor.Green);

            waiter.IsBusy = false;

            Thread.Sleep(2 * Settings.Settings.TimeUnit);
            table.Status = Status.Available;
        }
        else
        {
            Console.WriteLine("Failed to serve order with id: " + finishedOrder.Id);
        }
    }
}