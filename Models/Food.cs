public class Food
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int PreparationTime { get; set; }

    public Food() { }

    public Food(int id, string name, int preparationTime)
    {
        Id = id;
        Name = name;
        PreparationTime = preparationTime;
    }
}