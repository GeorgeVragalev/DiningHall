using System.Text;
using DiningHall.Helpers;
using DiningHall.Models;
using Newtonsoft.Json;

namespace DiningHall.Services.RestaurantService;

public class RestaurantService : IRestaurantService
{
   public async Task RegisterRestaurant(IList<Food> menu, double rating)
   {
      var restaurantData = new RestaurantData()
      {
         Id = 1,
         Menu = menu,
         Rating = rating,
         RestaurantName = "Andy's Pizza"
      };
      
      try
      {
         var json = JsonConvert.SerializeObject(restaurantData);
         var data = new StringContent(json, Encoding.UTF8, "application/json");

         var url = Settings.Settings.GlovoUrl+"/register";
         using var client = new HttpClient();

         await client.PostAsync(url, data);
         PrintConsole.Write($"Registered restaurant {restaurantData.RestaurantName}",
            ConsoleColor.Green);
      }
      catch (Exception e)
      {
         PrintConsole.Write($"Failed to register restaurant {restaurantData.RestaurantName}", ConsoleColor.DarkRed);
      }
   }
}