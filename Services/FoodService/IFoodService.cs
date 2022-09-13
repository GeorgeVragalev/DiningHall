using System.Collections.Concurrent;
using DiningHall.Models;

namespace DiningHall.Services.FoodService;

public interface IFoodService
{
    public ConcurrentBag<Food> GenerateFood();
    public Task<Food> GetFoodById(int id);
    public Task<IList<int>> GenerateOrderFood();
}