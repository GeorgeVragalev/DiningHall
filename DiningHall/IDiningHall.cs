using DiningHall.Models;

namespace DiningHall.DiningHall;

public interface IDiningHall
{
    public void RunRestaurant();
    public void ServeOrder(FinishedOrder finishedOrder);
}