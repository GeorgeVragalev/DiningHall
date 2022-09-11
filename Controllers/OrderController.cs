using DiningHall.DiningHall;
using DiningHall.Models;
using DiningHall.Repositories.FoodRepository;
using Microsoft.AspNetCore.Mvc;

namespace DiningHall.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Route("/distribution")]
public class OrderController : ControllerBase
{
    private readonly IDiningHall _diningHall;
    private readonly IFoodRepository _foodRepository;

    public OrderController(IDiningHall diningHall, IFoodRepository foodRepository)
    {
        _diningHall = diningHall;
        _foodRepository = foodRepository;
    }

    [HttpPost]
    public FinishedOrder Distribution([FromBody] Order order)
    {
        //Process order
        //Call Dininghall function to process the Order and serve it back
        _diningHall.ServeOrder(new FinishedOrder());

        return new FinishedOrder();
    }

    [HttpGet]
    public Food GetFood(int id)
    {
        return _foodRepository.GetFoodById(id);
    }

}