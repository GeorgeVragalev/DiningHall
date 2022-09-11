using DiningHall.Models;
using DiningHall.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace DiningHall.Repositories.Generic;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly AppDbContext _context;
    private readonly DbSet<TEntity> _dbSet; 
    public IQueryable<TEntity> Table => _dbSet.AsQueryable();

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity?> GetById(int id)
    {
        return await Table.Where(it => it.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await Table.ToListAsync();
    }

    public async Task Insert(TEntity item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        _dbSet.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task Update(TEntity item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(TEntity item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        _dbSet.Remove(item);
        await _context.SaveChangesAsync();
    }
}