namespace DiningHall.Repositories.Generic;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task Insert(TEntity item);
    Task Update(TEntity item);
    Task Delete(TEntity item);

    Task<TEntity?> GetById(int id);
    Task<IEnumerable<TEntity>> GetAll();
    IQueryable<TEntity> Table { get; }
}