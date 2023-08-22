using AutoMapper;
using BookStore.App.Books.Models;
using BookStore.App.Infrastructure.Mapping.Interfaces;
using BookStore.Domain;
using NodaTime;

namespace BookStore.App.Orders.Models;

public class OrderVm : IHaveCustomMapping
{    
    public int Id { get; set; }
    public LocalDateTime OrderDataTime { get; set; }
    public ICollection<BookVm>? Books { get; set; }
    public void CreateMappings(Profile configuration)
    {
        configuration
            .CreateMap<Order, OrderVm>()
            .ReverseMap();
    }
}