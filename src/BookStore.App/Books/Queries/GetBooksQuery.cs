using BookStore.App.Books.Models;
using BookStore.App.Infrastructure.Exceptions;
using BookStore.App.Infrastructure.Interfaces;
using BookStore.App.Infrastructure.Mapping.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace BookStore.App.Books.Queries;

public class GetBooksQuery : IRequest<List<BookVm>>
{
    public string? Name { get; set; }
    
    public LocalDate? Date { get; set; }
}


public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<BookVm>>
{
    private readonly IBookRepository _repository;

    public GetBooksQueryHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<BookVm>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var res = _repository.GetBooksQuery(request);

        if (res == default)
        {
            throw new CustomException();
        }
        
        return await res.ToListAsync(cancellationToken: cancellationToken);
    }
}