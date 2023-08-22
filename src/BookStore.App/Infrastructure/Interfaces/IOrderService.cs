using BookStore.App.Infrastructure.Mapping.Models;
using BookStore.App.Infrastructure.Models;

namespace BookStore.App.Infrastructure.Interfaces;

public interface IOrderService
{
    Task<int> AddOrder(OrderVm orderVm);
    Task UpdateOrder(OrderVm orderVm);
    IQueryable<OrderVm> GetOrdersQuery(GetOrderQuery query);
    Task<OrderVm> GetById(int id);
    Task DeleteOrder(int id);
}