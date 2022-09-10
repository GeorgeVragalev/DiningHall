namespace DiningHall.Models;

public class Table
{
    public int Id { get; set; }
    public Waiter WaiterId { get; set; }
    public Status Status { get; set; }
}