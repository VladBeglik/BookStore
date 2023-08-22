namespace BookStore.App.Infrastructure.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T> GetById(int id);
}