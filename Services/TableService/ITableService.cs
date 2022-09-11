using DiningHall.Models;

namespace DiningHall.Services.TableService;

public interface ITableService
{
    public int GetFreeTableId();
    public IList<Table> GenerateTables();
}