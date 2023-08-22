using BookStore.App.Infrastructure.Mapping.Models;
using BookStore.App.Infrastructure.Models;
using BookStore.Domain;

namespace BookStore.App.Infrastructure.Interfaces;

public interface IBookRepository : IRepository<Book>
{
    IQueryable<BookVm> GetBooksQuery(GetBooksQuery query);
}