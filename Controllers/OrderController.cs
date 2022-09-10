using DiningHall.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiningHall.Controllers;

[ApiController]
[Route("distribution")]
public class OrderController : ControllerBase
{
    [HttpPost]
    public ActionResult Distribution([FromBody] FinishedOrder order)
    {
        //Process order
        //Call Dininghall function to process the Order and serve it back
        
        
        
        return new JsonResult(order);
    }
}