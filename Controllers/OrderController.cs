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
        //Process order
        //Call Dininghall function to process the Order and serve it back
        Console.WriteLine("Order "+ order.Id+" received");
        _diningHall.ServeOrder(order);

        // return new FinishedOrder();
    }
    
    [HttpGet]
    public ContentResult Get()
    {
        return Content("Hi");
    }
}