using System.Collections.Concurrent;
using DiningHall.Helpers;
using DiningHall.Models;

namespace DiningHall.Repositories.FoodRepository;

public class FoodRepository : IFoodRepository
{
    private readonly ConcurrentBag<Food> _menu = new ConcurrentBag<Food>();
    
    public ConcurrentBag<Food> GenerateMenu()
    {
        _menu.Add(new Food
        {
            Id = 1,
            Name = "Pizza",
            PreparationTime = 20
        });
        _menu.Add(new Food
        {
            Id = 2,
            Name = "Salad",
            PreparationTime = 10
        });
        _menu.Add(new Food
        {
            Id = 3,
            Name = " Zeama",
            PreparationTime = 7
        });
        _menu.Add(new Food
        {
            Id = 4,
            Name = "Scallop Sashimi with Meyer Lemon Confit",
            PreparationTime = 32
        });
        _menu.Add(new Food
        {
            Id = 5,
            Name = "Island Duck with Mulberry Mustard",
            PreparationTime = 35
        });
        _menu.Add(new Food
        {
            Id = 6,
            Name = "Waffles",
            PreparationTime = 10
        });
        // _menu.Add(new Food
        // {
        //     Id = 7,
        //     Name = "Aubergine",
        //     PreparationTime = 20
        // });
        // _menu.Add(new Food
        // {
        //     Id = 8,
        //     Name = "Lasagna",
        //     PreparationTime = 30
        // });
        // _menu.Add(new Food
        // {
        //     Id = 9,
        //     Name = "Burger",
        //     PreparationTime = 15
        // });
        // _menu.Add(new Food
        // {
        //     Id = 10,
        //     Name = "Gyros",
        //     PreparationTime = 15
        // });
        //
        // _menu.Add(new Food
        // {
        //     Id = 11,
        //     Name = "Kebab",
        //     PreparationTime = 15
        // });
        // _menu.Add(new Food
        // {
        //     Id = 12,
        //     Name = "UnagiMaki",
        //     PreparationTime = 20
        // });
        // _menu.Add(new Food
        // {
        //     Id = 13,
        //     Name = "TobaccoChicken",
        //     PreparationTime = 30
        // });

        return _menu;
    }

    public async Task<Food> GetFoodById(int id)
    {
        return await Task.FromResult(_menu.FirstOrDefault(food => food.Id.Equals(id))!);
    }
}