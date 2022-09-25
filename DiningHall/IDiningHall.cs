using DiningHall.Models;

namespace DiningHall.DiningHall;

public interface IDiningHall
{
    public void ExecuteCode(CancellationToken cancellationToken);
    public void ServeOrder(FinishedOrder finishedOrder);
}