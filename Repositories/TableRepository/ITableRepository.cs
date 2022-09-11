using System.Collections.Concurrent;
using DiningHall.Models;

namespace DiningHall.Repositories.TableRepository;

public interface ITableRepository
{
    public ConcurrentBag<Table> GenerateTables();
    public Table GetFreeTable();
}