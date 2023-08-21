using BookStore.App.Infrastructure.Mapping.Models;

namespace BookStore.App.Infrastructure.Interfaces;

public interface IBookService
{
    Task<int> AddBook(BookVm bookVm);
    Task UpdateBook(BookVm bookVm);
    Task<IQueryable<BookVm>> GetBooksQuery();
    Task<BookVm> GetById(int id);
    Task DeleteBook(int id);
}