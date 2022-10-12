using DiningHall.Models;
using DiningHall.Services.FoodService;

namespace DiningHall.Helpers;

public static class OrderExtension
{
    private static IList<int> ratings = new List<int>();
    public static FinishedOrder MapFinishedOrder(this Order order)
    {
        var finishedOrder = new FinishedOrder()
        {
            Id = order.Id,
            Priority = order.Priority,
            Foods = order.Foods,
            CookingDetails = new List<CookingDetails>(),
            MaxWait = order.MaxWait,
            CookingTime = 0,
            TableId = order.TableId,
            WaiterId = order.WaiterId,
        };
        return finishedOrder;
    }

    public static double GetOrderRating(this FinishedOrder order)
    {
        var waitingTime = (DateTime.Now - order.PickUpTime);

        var timeElapsed = SetTimeElapsed(waitingTime);
        var rating = GetRating(timeElapsed, order.MaxWait);
        
        ratings.Add(rating);
        PrintConsole.Write($"Received rating {rating} from orderId {order.Id} {timeElapsed} | {order.MaxWait}", ConsoleColor.DarkGreen);

        return ratings.Average();
    }

    private static int SetTimeElapsed(TimeSpan time)
    {
        var timeElapsed = 0;

        switch (Settings.Settings.TimeUnit)
        {
            case 1:
                timeElapsed = (int) time.TotalMilliseconds;
                break;
            case 1000:
                timeElapsed = (int) time.TotalSeconds;
                break;
            case 60000:
                timeElapsed = (int) time.TotalMinutes;
                break;
        }

        return timeElapsed;
    }
    private static int GetRating(int timeElapsed, int maxWait)
    {
        
        var rating = 1;

        if (timeElapsed < maxWait)
        {
            rating = 5;
        }
        else if (timeElapsed < maxWait * 1.1)
        {
            rating = 4;
        }
        else if (timeElapsed < maxWait * 1.2)
        {
            rating = 3;
        }
        else if (timeElapsed < maxWait * 1.3)
        {
            rating = 2;
        }

        return rating;
    }
    
    public static int CalculateMaxWaitingTime(this IEnumerable<int> foodList, IFoodService foodService)
    {
        var maxWaitingTime = 0;
        foreach (var foodId in foodList)
        {
            var food = foodService.GetFoodById(foodId).Result;
            maxWaitingTime += food.PreparationTime;
        }

        return (int) Math.Ceiling(maxWaitingTime * 1.3);
    }
}