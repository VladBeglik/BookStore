using BookStore.App.Books.Models;
using BookStore.App.Books.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers;

public class BookController : MediatrController
{
    
    /// <summary>
    /// Получить книгу по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBookById(int id)
    {
        var book = await Mediator.Send(new GetBookByIdQuery{Id = id});
        return Ok(book);
    }
    
    
    /// <summary>
    /// Получить список книг
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<BookVm>> GetBooks([FromBody] GetBooksQuery query)
    {
        var res = await Mediator.Send(query);
        return res;
    }
    
}