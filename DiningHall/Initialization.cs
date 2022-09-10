using DiningHall.Repositories.FoodRepository;
using DiningHall.Repositories.TableRepository;
using DiningHall.Repositories.WaiterRepository;

namespace DiningHall.DiningHall;

public static class Initialization
{
   public static void Start()
   {
      var diningHall = new DiningHall(new TableRepository(), new WaiterRepository(), new FoodRepository());
      diningHall.RunRestaurant();
   }
}