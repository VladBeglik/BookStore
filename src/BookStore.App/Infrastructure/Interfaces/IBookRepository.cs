using BookStore.App.Books.Models;
using BookStore.App.Books.Queries;
using BookStore.Domain;

namespace BookStore.App.Infrastructure.Interfaces;

public interface IBookRepository : IRepository<Book>
{
    IQueryable<BookVm> GetBooksQuery(GetBooksQuery query);

    Task<IList<Book>> GetBooksByIds(int[] ids);
}