using NodaTime;

namespace BookStore.Domain;

public class Book : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Author { get; set; } = null!;
    public LocalDate ReleaseDate { get; set; }
    public decimal Price { get; set; }
    
    public ICollection<Order>? Orders { get; set; }
}