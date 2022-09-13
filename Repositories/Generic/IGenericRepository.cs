namespace DiningHall.Repositories.Generic;

public interface IGenericRepository<T> where T : class
{
    public Task<T> GetById(int id);
}