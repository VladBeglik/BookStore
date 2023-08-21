using AutoMapper;
using BookStore.App.Infrastructure.Exceptions;
using BookStore.App.Infrastructure.Interfaces;
using BookStore.App.Infrastructure.Mapping.Models;
using BookStore.Domain;
using FluentValidation;

namespace BookStore.App.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidationHelper<BookVm> _bookValidatorHelper;
    
    public BookService(IBookRepository repository, IMapper mapper, IValidator<BookVm> validator, IValidationHelper<BookVm> bookValidatorHelper)
    {
        _repository = repository;
        _mapper = mapper;
        _bookValidatorHelper = bookValidatorHelper;
    }

    public async Task<int> AddBook(BookVm bookVm)
    {

        await _bookValidatorHelper.ValidateAndThrowAsync(bookVm);
        
        var book = _mapper.Map<Book>(bookVm);
        var id = await _repository.Add(book);
        return id;
    }

    public async Task UpdateBook(BookVm bookVm)
    {

        await _bookValidatorHelper.ValidateAndThrowAsync(bookVm);
        
        var book = _mapper.Map<Book>(bookVm);
        await _repository.Update(book);
    }

    public async Task<IQueryable<BookVm>> GetBooksQuery()
    {
        throw new NotImplementedException();
    }

    public async Task<BookVm> GetById(int id)
    {
       var book =  await _repository.GetById(id);
       return _mapper.Map<BookVm>(book);
    }

    public async Task DeleteBook(int id)
    {
        await _repository.Delete(id);
    }
}