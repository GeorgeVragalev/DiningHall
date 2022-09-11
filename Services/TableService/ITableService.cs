using System.Collections.Concurrent;
using DiningHall.Models;

namespace DiningHall.Services.TableService;

public interface ITableService
{
    public int GetFreeTableId();
    public ConcurrentBag<Table> GenerateTables();
}