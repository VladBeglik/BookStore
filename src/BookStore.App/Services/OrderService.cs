using AutoMapper;
using BookStore.App.Infrastructure.Interfaces;
using BookStore.App.Infrastructure.Mapping.Models;
using BookStore.App.Infrastructure.Models;
using BookStore.Domain;

namespace BookStore.App.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidationHelper<OrderVm> _orderValidatorHelper;


    public OrderService(IOrderRepository orderRepository, IMapper mapper, IValidationHelper<OrderVm> orderValidatorHelper)
    {
        _repository = orderRepository;
        _mapper = mapper;
        _orderValidatorHelper = orderValidatorHelper;
    }

    public async Task<int> AddOrder(OrderVm orderVm)
    {
        await _orderValidatorHelper.ValidateAndThrowAsync(orderVm);

        
        var book = _mapper.Map<Order>(orderVm);
        var id = await _repository.Add(book);
        return id;
    }

    public async Task UpdateOrder(OrderVm orderVm)
    {
        await _orderValidatorHelper.ValidateAndThrowAsync(orderVm);

        
        var book = _mapper.Map<Order>(orderVm);
        await _repository.Update(book);
        
    }

    public IQueryable<OrderVm> GetOrdersQuery(GetOrderQuery query)
    {
        return _repository.GetOrdersQuery(query);
    }

    public async Task<IQueryable<OrderVm>> GetOrdersQuery()
    {
        throw new NotImplementedException();

    }

    public async Task<OrderVm> GetById(int id)
    {
        var book =  await _repository.GetById(id);
        return _mapper.Map<OrderVm>(book);
    }

    public async Task DeleteOrder(int id)
    {
        await _repository.Delete(id);
    }
}
