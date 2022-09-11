using DiningHall.Models;

namespace DiningHall.Helpers;

public static class OrderMappingExtension
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
}