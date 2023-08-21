using BookStore.App.Infrastructure;
using BookStore.App.Infrastructure.Interfaces;
using BookStore.Domain;
using BookStore.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Persistence;

public class OrderRepository : IOrderRepository
{
    private readonly IBookStoreDbContext _ctx;

    public OrderRepository(IBookStoreDbContext ctx)
    {
        _ctx = ctx;
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

    public async Task Update(Order order)
    {
        _ctx.Orders.Update(order);
        await _ctx.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var order = await _ctx.Orders.FirstOrDefaultAsync(_ => _.Id == id);
        if (order != null) _ctx.Orders.Remove(order);
        await _ctx.SaveChangesAsync();
    }

    public Task<IQueryable<Order>> GetOrdersQuery()
    {
        throw new NotImplementedException();
    }
}