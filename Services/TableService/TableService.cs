using System.Collections.Concurrent;
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

    public ConcurrentBag<Table> GenerateTables()
    {
        return _tableRepository.GenerateTables();
    }

    public async Task<Table> GetFreeTable()
    {
        var table = await _tableRepository.GetFreeTable();

        if (table!=null)
        {
            return await Task.FromResult(table);
        }
        return null!;
    }
    
    public async Task<Table> GetTableById(int? id)
    {
        var table = await _tableRepository.GetTableById(id);

        if (table!=null)
        {
            return await Task.FromResult(table);
        }
        return null!;
    }
}