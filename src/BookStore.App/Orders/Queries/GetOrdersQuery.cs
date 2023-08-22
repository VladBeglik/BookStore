using BookStore.App.Infrastructure.Exceptions;
using BookStore.App.Infrastructure.Interfaces;
using BookStore.App.Infrastructure.Mapping.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace BookStore.App.Orders.Queries;

public class GetOrdersQuery : IRequest<List<OrderVm>>
{
    public int? Id { get; set; }
    
    public LocalDateTime? Date { get; set; }
}

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<OrderVm>>
{
    private readonly IOrderRepository _repository;

    public GetOrdersQueryHandler(IOrderRepository repository)
    {
        _repository = repository;
    }


    public async Task<List<OrderVm>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var res = _repository.GetOrdersQuery(request);

        if (res == default)
        {
            throw new CustomException();
        }
        
        return await res.ToListAsync(cancellationToken: cancellationToken);
    }
}