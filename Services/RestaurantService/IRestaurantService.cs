
using DiningHall.Models;

namespace DiningHall.Services.RestaurantService;

public interface IRestaurantService
{
    public Task RegisterRestaurant(IList<Food> menu, double rating);
}