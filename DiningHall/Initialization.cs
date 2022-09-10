using DiningHall.Repositories.FoodRepository;
using DiningHall.Repositories.OrderRepository;
using DiningHall.Repositories.TableRepository;
using DiningHall.Repositories.WaiterRepository;
using DiningHall.Services;

namespace DiningHall.DiningHall;

public static class Initialization
{
   public static void Start()
   {
      var diningHall = new DiningHall(new TableRepository(), new WaiterRepository(), new FoodRepository(), new OrderService(new OrderRepository(new FoodRepository())));
      diningHall.RunRestaurant();
   }
}