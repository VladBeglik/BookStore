using NodaTime;

namespace BookStore.App.Infrastructure.Models;

public class GetBooksQuery
{
    public string? Name { get; set; }
    public LocalDate? LocalDate { get; set; }
}