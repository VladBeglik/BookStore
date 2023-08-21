using BookStore.App.Infrastructure.Mapping.Models;

namespace BookStore.App.Infrastructure.Interfaces;

public interface IOrderService
{
    Task<int> AddOrder(OrderVm orderVm);
    Task UpdateOrder(OrderVm orderVm);
    Task<IQueryable<OrderVm>> GetOrdersQuery();
    Task<OrderVm> GetById(int id);
    Task DeleteOrder(int id);
}