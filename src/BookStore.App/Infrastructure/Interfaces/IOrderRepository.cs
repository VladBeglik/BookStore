using BookStore.App.Infrastructure.Mapping.Models;
using BookStore.App.Infrastructure.Models;
using BookStore.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookStore.App.Infrastructure.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    IQueryable<OrderVm> GetOrdersQuery(GetOrderQuery query);
}