using BookStore.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookStore.App.Infrastructure.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<IQueryable<Order>> GetOrdersQuery();
}