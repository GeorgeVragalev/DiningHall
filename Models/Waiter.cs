namespace DiningHall.Models;

public class Waiter
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsBusy { get; set; }
    public Order Order { get; set; }
    
    public List<Order> ActiveOrders { get; set; }
    public List<int> CompletedOrderIds { get; set; }
}