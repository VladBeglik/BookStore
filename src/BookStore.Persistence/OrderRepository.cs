using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.App.Infrastructure.Interfaces;
using BookStore.App.Infrastructure.Mapping.Models;
using BookStore.App.Orders.Queries;
using BookStore.Domain;
using BookStore.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Persistence;

public class OrderRepository : IOrderRepository
{
    private readonly IBookStoreDbContext _ctx;
    private readonly IMapper _mapper;
    public OrderRepository(IBookStoreDbContext ctx, IMapper mapper)
    {
        _ctx = ctx;
        _mapper = mapper;
    }

    public async Task<Order> GetById(int id)
    {
        return await _ctx.Orders.FirstOrDefaultAsync(_ => _.Id == id);
    }

    public async Task<int> Add(Order order)
    {
        _ctx.Orders.Add(order);
        await _ctx.SaveChangesAsync();
        return order.Id;
    }


    public IQueryable<OrderVm> GetOrdersQuery(GetOrdersQuery query)
    {
        var orders =  _ctx.Orders.AsQueryable();

        if (query.Id.HasValue)
        {
            orders = orders.Where(_ => _.Id == query.Id);
        }

        if (query.Date.HasValue)
        {
            orders = orders.Where(_ => _.OrderDataTime == query.Date);
        }

        var res = orders.ToList();
        return orders.ProjectTo<OrderVm>(_mapper.ConfigurationProvider);
    }

    public async Task<int> SaveOrder(Order order)
    {
        _ctx.Orders.Add(order);
        await _ctx.SaveChangesAsync();
        return order.Id;
    }
}