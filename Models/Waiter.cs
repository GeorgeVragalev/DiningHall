namespace DiningHall.Models;

public class Waiter : BaseEntity
{
    public string Name { get; set; }
    public bool IsBusy { get; set; }
    public List<int> ActiveOrdersIds { get; set; }
    public List<int> CompletedOrderIds { get; set; }
}