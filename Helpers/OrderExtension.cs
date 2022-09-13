using DiningHall.Models;

namespace DiningHall.Helpers;

public static class OrderExtension
{
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

    public static decimal GetOrderRating(this FinishedOrder order)
    {
        var waitingTime = (DateTime.Now - order.PickUpTime).Seconds;

        var timeElapsed = order.MaxWait - waitingTime;
        switch (timeElapsed)
        {
            case > 5:
                return 5;
            case <5:
                return 4;
            default:
                return 1;
        }
    }
}