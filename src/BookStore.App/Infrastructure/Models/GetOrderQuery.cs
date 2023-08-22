using NodaTime;

namespace BookStore.App.Infrastructure.Models;

public class GetOrderQuery
{
    public int? Id { get; set; }
    public LocalDateTime? Date { get; set; }
}