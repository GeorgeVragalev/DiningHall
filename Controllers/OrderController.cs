using DiningHall.DiningHall;
using DiningHall.Models;
using DiningHall.Repositories.FoodRepository;
using Microsoft.AspNetCore.Mvc;

namespace DiningHall.Controllers;

[ApiController]
[Route("/distribution")]
public class OrderController : ControllerBase
{
    private readonly IDiningHall _diningHall;
    private readonly IFoodRepository _foodRepository;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IDiningHall diningHall, IFoodRepository foodRepository, ILogger<OrderController> logger)
    {
        _diningHall = diningHall;
        _foodRepository = foodRepository;
        _logger = logger;
    }

    [HttpPost]
    public void Distribution([FromBody] FinishedOrder order)
    {
        //Process order
        //Call Dininghall function to process the Order and serve it back
        _logger.LogInformation("Order "+ order.Id+" received");
        _diningHall.ServeOrder(order);

        // return new FinishedOrder();
    }

}