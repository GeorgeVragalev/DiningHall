using System.Collections.Concurrent;
using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Repositories.FoodRepository;

namespace DiningHall.Services.FoodService;

public class FoodService : IFoodService
{
    private readonly IFoodRepository _foodRepository;

    public FoodService(IFoodRepository foodRepository)
    {
        _foodRepository = foodRepository;
    }

    public  ConcurrentBag<Food> GenerateFood()
    {
        return _foodRepository.GenerateMenu();
    }

    public Task<Food> GetFoodById(int id)
    {
        return _foodRepository.GetFoodById(id);
    }

    public async Task<IList<int>> GenerateOrderFood()
    {
        var size = RandomGenerator.NumberGenerator(6);
        var orderFoodList = new List<int>();

        for (var id = 0; id < size; id++)
        {
            orderFoodList.Add(RandomGenerator.NumberGenerator(6));
        }

        return await Task.FromResult(orderFoodList);
    }
}