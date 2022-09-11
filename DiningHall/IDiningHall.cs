using DiningHall.Models;

namespace DiningHall.DiningHall;

public interface IDiningHall
{
    public void RunRestaurant(CancellationToken cancellationToken);
    public void ServeOrder(FinishedOrder finishedOrder);
}