using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.App.Books.Models;
using BookStore.App.Books.Queries;
using BookStore.App.Infrastructure;
using BookStore.App.Infrastructure.Exceptions;
using BookStore.App.Infrastructure.Interfaces;
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

        if (query.Date.HasValue)
        {
            books = books.Where(_ => _.ReleaseDate == query.Date);
        }

        return books.ProjectTo<BookVm>(_mapper.ConfigurationProvider);
    }

    public async Task<IList<Book>> GetBooksByIds(int[] ids)
    {
        var books = await _ctx.Books.Where(_ => ids.Contains(_.Id)).ToListAsync();

        if (books.Count == default)
        {
            throw new CustomException(ExMsg.Book.NotFound());
        }
        
        return books;
    }

    public async Task<Book> GetById(int id)
    {
        return await _ctx.Books.FirstOrDefaultAsync(_ => _.Id == id);
    }
}