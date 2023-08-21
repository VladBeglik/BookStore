using AutoMapper;
using BookStore.App.Infrastructure.Mapping.Interfaces;
using BookStore.Domain;
using NodaTime;

namespace BookStore.App.Infrastructure.Mapping.Models;

public class OrderVm : IHaveCustomMapping
{    
    public int Id { get; set; }
    public LocalDateTime OrderDataTime { get; set; }
    public ICollection<Book>? Books { get; set; }
    public void CreateMappings(Profile configuration)
    {
        configuration
            .CreateMap<Order, OrderVm>()
            .ReverseMap();
    }
}