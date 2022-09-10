namespace DiningHall.Models;

public class Order : BaseEntity
{
    public int TableId { get; set; }
    public int WaiterId { get; set; } 
    public int Priority { get; set; }
    public int MaxWait { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public IList<int> Foods { get; set; }

    public Order() { }
}