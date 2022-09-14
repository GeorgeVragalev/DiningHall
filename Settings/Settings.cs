namespace DiningHall.Settings;

public static class Settings
{
    public static readonly int Tables = 10;
    public static readonly int Waiters = 5;
    public static readonly string KitchenUrl = "http://localhost:5001/order"; //docker
    // public static readonly string KitchenUrl = "https://localhost:7091/order"; //local
    public static readonly string TimeUnit= "Seconds";
}