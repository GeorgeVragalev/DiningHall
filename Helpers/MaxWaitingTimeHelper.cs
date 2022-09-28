using DiningHall.Repositories.FoodRepository;
using DiningHall.Services.FoodService;

namespace DiningHall.Helpers;

public static class MaxWaitingTimeHelper
{
    public static int CalculateMaxWaitingTime(this IEnumerable<int> foodList, IFoodService foodService)
    {
        var maxWaitingTime = 0;
        foreach (var foodId in foodList)
        {
            var food = 1; /*foodService.GetFoodById(foodId).Result;*/
            maxWaitingTime += 20;
        }

        return (int) (maxWaitingTime * 1.3);
    }
}