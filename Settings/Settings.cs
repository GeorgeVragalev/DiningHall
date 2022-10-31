using DiningHall.Models;

namespace DiningHall.Settings;

public static class Settings
{
    public static RestaurantData Restaurant = new RestaurantData();
    public static readonly int RestaurantId = 1; 
    public static readonly string RestaurantName = "Andy's Pizza"; 

    public static readonly int Tables = 6;
    public static readonly int Waiters = 3;

    // public static readonly string KitchenUrl = "http://host.docker.internal:7091/order"; //docker
    // public static readonly string GlovoUrl = "http://host.docker.internal:7068"; //docker
    // public static readonly string RestaurantUrl = "http://host.docker.internal:7090"; //docker

    public static readonly string KitchenUrl = "https://localhost:7091/order"; //local
    public static readonly string GlovoUrl = "https://localhost:7068"; //local
    public static readonly string RestaurantUrl = "https://localhost:7090"; //local
    
    public static readonly int TimeUnit = 1; //seconds = 1000  ms = 1 minutes = 60000 
}
/*
to run docker for dininghall container: 
BUILD IMAGE:
docker build -t dininghall .

RUN CONTAINER: map local_port:exposed_port
docker run --name dininghall-container -p 7090:80 dininghall
*/