using DiningHall.Models;

namespace DiningHall.Services;

public interface ITableService
{
    public int GetFreeTableId();
    public IList<Table> GenerateTables();
}