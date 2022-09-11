using System.Collections.Concurrent;
using DiningHall.Models;

namespace DiningHall.Repositories.FoodRepository;

public interface IFoodRepository
{
    public ConcurrentBag<Food> GenerateFood();
    public Food GetFoodById(int id);
    public IList<int> GenerateOrderFood();
}