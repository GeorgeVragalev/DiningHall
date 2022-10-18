using DiningHall.Models;

namespace DiningHall.DiningHall;

public interface IDiningHall
{
    public void ExecuteCode(CancellationToken cancellationToken);
    public Task ServeOrder(FinishedOrder finishedOrder);
}