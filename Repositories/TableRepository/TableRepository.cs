using System.Collections.Concurrent;
using DiningHall.Models;

namespace DiningHall.Repositories.TableRepository;

public class TableRepository : ITableRepository
{
    private readonly ConcurrentBag<Table> _tables =  new();

    public ConcurrentBag<Table> GenerateTables()
    {
        var maxTables = Settings.Settings.Tables;
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
    
    public async Task<Table> GetFreeTable()
    {
        foreach (var table in _tables)
        {
            if (table.Status == Status.Available)
            {
                return await Task.FromResult(table);
            }
        }

        return null!;
    }

    public async Task<Table> GetTableById(int id)
    {
        foreach (var table in _tables)
        {
            if (table.Id == id)
            {
                return await Task.FromResult(table);
            }
        }

        return null!;
    }
}