using BookStore.App.Orders.Commands;
using BookStore.App.Orders.Models;
using BookStore.App.Orders.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers;

public class OrderController : MediatrController
{
   
    /// <summary>
    /// Получить заказ по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await Mediator.Send(new GetOrderByIdQuery { Id = id });
        return Ok(order);
    }

    /// <summary>
    /// получить список заказов
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<OrderVm>> GetOrders([FromBody] GetOrdersQuery query)
    {
        var res = await Mediator.Send(query);
        return res;
    }
    
    /// <summary>
    /// Сохранить заказ
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("save")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<int> SaveOrder(SaveOrderCommand r)
    {
        var res = await Mediator.Send(r);
        return res;
    }
    
    
    
}