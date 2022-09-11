namespace DiningHall.Helpers;

public static class GetReady
{
    public static bool IsReady(int isReady)
    {
        if (isReady == 0)
        {
            return false;
        }
        return true;
    }
}