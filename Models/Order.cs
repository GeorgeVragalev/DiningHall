namespace DiningHall.Models;

public class Order
{
    public int Id { get; set; }
    public int TableId { get; set; }
    public int WaiterId { get; set; }
    public int MaxWait { get; set; }
    public IList<int> Foods { get; set; }

    public Order()
    {
    }
}