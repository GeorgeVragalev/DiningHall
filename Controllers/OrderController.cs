using DiningHall.DiningHall;
using DiningHall.Models;
using Microsoft.AspNetCore.Mvc;

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
    public void Distribution([FromBody] FinishedOrder order)
    {
        Console.WriteLine("Order "+ order.Id+" received");
        _diningHall.ServeOrder(order);
    }
    
    [HttpGet]
    public ContentResult Get()
    {
        return Content("Hi");
    }
}