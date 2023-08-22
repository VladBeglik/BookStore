using AutoMapper;
using BookStore.App.Infrastructure.Exceptions;
using BookStore.App.Infrastructure.Interfaces;
using BookStore.App.Orders.Models;
using MediatR;

namespace BookStore.App.Orders.Queries;

public class GetOrderByIdQuery : IRequest<OrderVm>
{
    public int Id { get; set; }
}


public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderVm>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<OrderVm> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetById(request.Id);

        if (order == default)
        {
            throw new CustomException();
        }

        return _mapper.Map<OrderVm>(order);
    }
}