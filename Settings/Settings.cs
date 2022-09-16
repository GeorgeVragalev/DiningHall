namespace DiningHall.Settings;

public static class Settings
{
    public static readonly int Tables = 10;
    public static readonly int Waiters = 5;
    public static readonly string KitchenUrl = "http://host.docker.internal:7091/order"; //docker
    // public static readonly string KitchenUrl = "https://localhost:7091/order"; //local
    public static readonly string TimeUnit= "Seconds";
}
/*
to run docker for dininghall container: 
BUILD IMAGE:
docker build -t dininghall .

RUN CONTAINER: map local_port:exposed_port
docker run --name dininghall-container -p 7090:80 dininghall
*/