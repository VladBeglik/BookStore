using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.App.Infrastructure;
using BookStore.App.Infrastructure.Interfaces;
using BookStore.App.Infrastructure.Mapping.Models;
using BookStore.App.Infrastructure.Models;
using BookStore.Domain;
using BookStore.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Persistence;

public class BookRepository : IBookRepository
{
    private readonly IBookStoreDbContext _ctx;
    private readonly IMapper _mapper;

    public BookRepository(IBookStoreDbContext ctx, IMapper mapper)
    {
        _ctx = ctx;
        _mapper = mapper;
    }

    public IQueryable<BookVm> GetBooksQuery(GetBooksQuery query)
    {
        var books =  _ctx.Books.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Name))
        {
            books = books.Where(_ => _.Name == query.Name);
        }

        if (query.LocalDate.HasValue)
        {
            books = books.Where(_ => _.ReleaseDate == query.LocalDate);
        }

        return books.ProjectTo<BookVm>(_mapper.ConfigurationProvider);
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