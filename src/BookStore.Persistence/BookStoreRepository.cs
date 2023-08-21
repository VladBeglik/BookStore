using BookStore.App.Infrastructure;
using BookStore.App.Infrastructure.Interfaces;
using BookStore.Domain;
using BookStore.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Persistence;

public class BookRepository : IBookRepository
{
    private readonly IBookStoreDbContext _ctx;

    public BookRepository(IBookStoreDbContext ctx)
    {
        _ctx = ctx;
    }

    public Task<IQueryable<Book>> GetBooksQuery()
    {
        throw new NotImplementedException();
    }

    public async Task<Book> GetById(int id)
    {
        return await _ctx.Books.FirstOrDefaultAsync(_ => _.Id == id);
    }

    public async Task<int> Add(Book book)
    {
        _ctx.Books.Add(book);
        await _ctx.SaveChangesAsync();
        return book.Id;
    }

    public async Task Update(Book book)
    {
        _ctx.Books.Update(book);
        await _ctx.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var book = await _ctx.Books.FirstOrDefaultAsync(_ => _.Id == id);
        if (book != null) _ctx.Books.Remove(book);
        await _ctx.SaveChangesAsync();
    }
}