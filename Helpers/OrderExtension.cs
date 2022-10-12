using DiningHall.Models;

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
        var timeElapsed = 0;

        switch (Settings.Settings.TimeUnit)
        {
            case 1:
                timeElapsed = waitingTime.Milliseconds;
                break;
            case 1000:
                timeElapsed = waitingTime.Seconds;
                break;
            case 60000:
                timeElapsed = waitingTime.Minutes;
                break;
        }
        
        var rating = 1;
        if (timeElapsed < order.MaxWait)
        {
            rating = 5;
        }
        else if (timeElapsed < order.MaxWait * 1.1)
        {
            rating = 4;
        }
        else if (timeElapsed < order.MaxWait * 1.2)
        {
            rating = 3;
        }
        else if (timeElapsed < order.MaxWait * 1.3)
        {
            rating = 2;
        }
        ratings.Add(rating);
        PrintConsole.Write($"Received rating {rating} from orderId {order.Id} {timeElapsed} | {order.MaxWait}", ConsoleColor.DarkGreen);

        return ratings.Average();
    }
}