using BookStore.App.Infrastructure;
using BookStore.App.Infrastructure.Exceptions;
using BookStore.App.Infrastructure.Interfaces;
using BookStore.Domain;
using FluentValidation;
using MediatR;
using NodaTime;

namespace BookStore.App.Orders.Commands;

public class SaveOrderCommand : IRequest<int>
{
    public int[]? BookIds { get; set; }
}


public class SaveOrderCommandValidator : AbstractValidator<SaveOrderCommand>
{
    public SaveOrderCommandValidator()
    {
        RuleFor(_ => _.BookIds).NotEmpty();
    }
}

public class SaveOrderCommandHandler : IRequestHandler<SaveOrderCommand, int>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IClock _clock;
    private readonly IBookRepository _bookRepository;
    public SaveOrderCommandHandler(IOrderRepository repository, IClock clock, IBookRepository bookRepository)
    {
        _orderRepository = repository;
        _clock = clock;
        _bookRepository = bookRepository;
    }

    public async Task<int> Handle(SaveOrderCommand request, CancellationToken cancellationToken)
    {

        var books = await _bookRepository.GetBooksByIds(request.BookIds!);

        if (books == default)
        {
            throw new CustomException();
        }

        var order = new Order
        {
            OrderDataTime = _clock.GetNow(),
            Books = books
        };

        var res = await _orderRepository.SaveOrder(order);

        return res;
    }
}