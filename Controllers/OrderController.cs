using System.Text;
using DiningHall.DiningHall;
using DiningHall.Helpers;
using DiningHall.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DiningHall.Controllers;

[ApiController]
[Route("/distribution")]
public class OrderController : ControllerBase
{
    private readonly IDiningHall _diningHall;

    public OrderController(IDiningHall diningHall)
    {
        _diningHall = diningHall;
    }

    [HttpPost]
    public async Task Distribution([FromBody] FinishedOrder order)
    {
        Console.WriteLine("Order "+ order.Id+" received");
        _diningHall.ServeOrder(order);
    }
    
    [HttpPost("/sendorder")]
    public async Task PickUpClientOrder([FromBody] ClientOrder order)
    {
        Console.WriteLine($"Order {order.Id} from group order {order.GroupOrderId} received in dining hall");
        // _diningHall.ServeOrder(order);
        
        try
        {
            var json = JsonConvert.SerializeObject(order);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = Settings.Settings.GlovoUrl+"/serve";
            using var client = new HttpClient();

            await client.PostAsync(url, data);
            PrintConsole.Write($"Order {order.Id} is prepared and sent to glovo", ConsoleColor.Green);
        }
        catch (Exception e)
        {
            PrintConsole.Write(Thread.CurrentThread.Name + " Failed to send order id: " + order.Id,
                ConsoleColor.DarkRed);
        }
    }
    
    [HttpGet]
    public ContentResult Get()
    {
        return Content("Hi");
    }
}