using BookStore.Domain;

namespace BookStore.App.Infrastructure.Interfaces;

public interface IBookRepository : IRepository<Book>
{
    Task<IQueryable<Book>> GetBooksQuery();
}