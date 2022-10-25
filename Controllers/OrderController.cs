using System.Text;
using DiningHall.DiningHall;
using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Models.Enum;
using DiningHall.Services.OrderService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DiningHall.Controllers;

[ApiController]
[Route("/distribution")]
public class OrderController : ControllerBase
{
    private readonly IDiningHall _diningHall;
    private readonly IOrderService _orderService;

    public OrderController(IDiningHall diningHall, IOrderService orderService)
    {
        _diningHall = diningHall;
        _orderService = orderService;
    }

    [HttpPost]
    public async Task Distribution([FromBody] FinishedOrder order)
    {

        if (order.OrderType == OrderType.DiningHallOrder)
        {
            Console.WriteLine("Order "+ order.Id+" received");
            // await _diningHall.ServeOrder(order);
        }
        else
        {
            Console.WriteLine($"Client Order {order.ClientId} received");
            //serve to client
            //send order to food service and mark it as cooked
            //or store client orders on repository here for pick up
        }
    }
    
    [HttpPost("/sendorder")]
    public async Task PickUpClientOrder([FromBody] Order order)
    {
        Console.WriteLine($"Order {order.Id} from group order {order.GroupOrderId} received in dining hall");
        // _diningHall.ServeOrder(order);
        order.Id = IdGenerator.GenerateId();
        await _orderService.SendOrder(order);
    }
    
    [HttpGet]
    public ContentResult Get()
    {
        return Content("Hi");
    }
}