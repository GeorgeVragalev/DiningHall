using DiningHall.Models.Enum;

namespace DiningHall.Models;

public class Table : BaseEntity
{ 
    public Status Status { get; set; }
}