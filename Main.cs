using DiningHall.Models;

namespace DiningHall;

public class Main
{
    public async Task main()
    {
        while (true)
        {
            Thread.Sleep(2300);
            Console.WriteLine("hello");
     
            HttpClient c = new HttpClient();
            c.BaseAddress = new Uri("https://localhost:7091/order");
            await c.PostAsJsonAsync("https://localhost:7091/order", new Order());
            
        }
    }
}