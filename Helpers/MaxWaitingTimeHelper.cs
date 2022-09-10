using DiningHall.Repositories.FoodRepository;

namespace DiningHall.Helpers;

public static class MaxWaitingTimeHelper
{
    public static int CalculateMaxWaitingTime(this IEnumerable<int> foodList, IFoodRepository repository)
    {
        var maxWaitingTime = 0;
        foreach (var foodId in foodList)
        {
            var food = repository.GetFoodById(foodId);
            maxWaitingTime += food.PreparationTime;
        }

        return (int) (maxWaitingTime * 1.3);
    }
}