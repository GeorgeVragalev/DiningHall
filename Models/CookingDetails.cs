namespace DiningHall.Models;

public class CookingDetails
{
    public int FoodId { get; set; }
    public int CookId { get; set; }

    public CookingDetails() { }

    public CookingDetails(int foodId, int cookId)
    {
        FoodId = foodId;
        CookId = cookId;
    }
}