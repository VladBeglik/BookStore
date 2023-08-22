using BookStore.App.Infrastructure.Mapping.Models;
using BookStore.App.Infrastructure.Models;
using BookStore.Domain;

namespace BookStore.App.Infrastructure.Interfaces;

public interface IBookService
{
    Task<int> AddBook(BookVm bookVm);
    Task UpdateBook(BookVm bookVm);
    IQueryable<BookVm> GetBooksQuery(GetBooksQuery query);
    Task<BookVm> GetById(int id);
    Task DeleteBook(int id);
}