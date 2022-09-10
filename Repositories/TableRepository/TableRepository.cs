using DiningHall.Models;

namespace DiningHall.Repositories.TableRepository;

public class TableRepository : ITableRepository
{
    private readonly IList<Table> _tables =  new List<Table>();

    public IList<Table> GenerateTables()
    {
        var maxTables = Models.Settings.Tables;
        for (var id = 1; id <= maxTables; id++)
        {
            _tables.Add(new Table
            {
                Id = id,
                Status = Status.Available
            });   
        }
        return _tables;
    }
}