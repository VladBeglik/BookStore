using NodaTime;

namespace BookStore.Domain;

public class Order : BaseEntity
{
    public LocalDateTime OrderDataTime { get; set; }
    public ICollection<Book>? Books { get; set; }
}