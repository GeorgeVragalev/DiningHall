using System.Collections.Concurrent;
using DiningHall.Models;

namespace DiningHall.Repositories.FoodRepository;

public interface IFoodRepository
{
    public ConcurrentBag<Food> GenerateMenu();
    public Task<Food> GetFoodById(int id);
}