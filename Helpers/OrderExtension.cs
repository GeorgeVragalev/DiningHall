﻿using DiningHall.Models;
using DiningHall.Services.FoodService;

namespace DiningHall.Helpers;

public static class OrderExtension
{
    private static IList<int> ratings = new List<int>();

    public static FinishedOrder MapFinishedOrder(this LocalOrder localOrder)
    {
        var finishedOrder = new FinishedOrder()
        {
            Id = localOrder.Id,
            Priority = localOrder.Priority,
            Foods = localOrder.Foods,
            CookingDetails = new List<CookingDetails>(),
            MaxWait = localOrder.MaxWait,
            CookingTime = 0,
            TableId = localOrder.TableId,
            WaiterId = localOrder.WaiterId,
        };

        return finishedOrder;
    }
    
    public static Order MapOrder(this FinishedOrder finishedOrder)
    {
        var order = new Order()
        {
            Id = finishedOrder.Id,
            Priority = finishedOrder.Priority,
            Foods = finishedOrder.Foods,
            ClientId = finishedOrder.ClientId,
            MaxWait = finishedOrder.MaxWait,
            OrderType = finishedOrder.OrderType,
            TableId = finishedOrder.TableId,
            WaiterId = finishedOrder.WaiterId,
            RestaurantId = finishedOrder.RestaurantId,
            GroupOrderId = finishedOrder.GroupOrderId,
            OrderStatusEnum = finishedOrder.OrderStatusEnum,
            PickUpTime = finishedOrder.PickUpTime
        };

        return order;
    }

    public static double AddClientRating(int rating)
    {
        ratings.Add(rating);
        return ratings.Average();
    }
    
    public static double GetOrderRating(this FinishedOrder order)
    {
        var waitingTime = (DateTime.Now - order.PickUpTime);
        var timeElapsed = SetTimeElapsed(waitingTime);
        var rating = GetRating(timeElapsed, order.MaxWait);

        ratings.Add(rating);
        // PrintConsole.Write($"Received rating {rating} from orderId {order.Id} {timeElapsed} | {order.MaxWait}", ConsoleColor.DarkGreen);

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