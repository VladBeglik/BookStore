namespace BookStore.App.Infrastructure.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T> GetById(int id);
    Task<int> Add(T entity);
    Task Update(T entity);
    Task Delete(int id);
}