using AutoMapper;
using BookStore.App.Books.Models;
using BookStore.App.Infrastructure.Exceptions;
using BookStore.App.Infrastructure.Interfaces;
using MediatR;

namespace BookStore.App.Books.Queries;

public class GetBookByIdQuery : IRequest<BookVm>
{
    public int Id { get; set; }
}

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookVm>
{
    private readonly IBookRepository _repository;
    private readonly IMapper _mapper;

    public GetBookByIdQueryHandler(IBookRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BookVm> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _repository.GetById(request.Id);
        
        if (book == default)
        {
            throw new CustomException();
        }
        
        return _mapper.Map<BookVm>(book);
    }
}