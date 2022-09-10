﻿using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Repositories.FoodRepository;

namespace DiningHall.Repositories.OrderRepository;

public class OrderRepository : IOrderRepository
{
    private readonly IFoodRepository _foodRepository;


    public OrderRepository(IFoodRepository foodRepository)
    {
        _foodRepository = foodRepository;
    }

    public Order GenerateOrder(int table, int waiter)
    {
        IList<int> foodList = _foodRepository.GenerateOrderFood();
        return new Order
        {
            Id = IdGenerator.GenerateId(),
            Priority = RandomGenerator.NumberGenerator(3),
            CreatedOnUtc = DateTime.UtcNow,
            Foods = foodList,
            TableId = table,
            WaiterId = waiter,
            MaxWait = foodList.CalculateMaxWaitingTime(_foodRepository)
        };
    }
}