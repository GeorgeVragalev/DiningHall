using DiningHall.Models;
using DiningHall.Repositories.TableRepository;

namespace DiningHall.Services.TableService;

public class TableService : ITableService
{
    private readonly ITableRepository _tableRepository;

    public TableService(ITableRepository tableRepository)
    {
        _tableRepository = tableRepository;
    }

    public IList<Table> GenerateTables()
    {
        return _tableRepository.GenerateTables();
    }

    public int GetFreeTableId()
    {
        var table = _tableRepository.GetFreeTable();

        if (table!=null)
        {
            return table.Id;
        }
        return 0;
    }
}