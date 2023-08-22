using BookStore.App.Orders.Models;
using BookStore.App.Orders.Queries;
using BookStore.Domain;

namespace BookStore.App.Infrastructure.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    IQueryable<OrderVm> GetOrdersQuery(GetOrdersQuery query);

    Task<int> SaveOrder(Order order);
}