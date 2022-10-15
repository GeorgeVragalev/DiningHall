using System.Collections.Concurrent;
using DiningHall.Models;

namespace DiningHall.Services.TableService;

public interface ITableService
{
    public Task<Table> GetFreeTable();
    public ConcurrentBag<Table> GenerateTables();
    public Task<Table> GetTableById(int? id);
}