using AutoMapper;
using BookStore.App.Infrastructure.Mapping.Interfaces;
using BookStore.Domain;
using NodaTime;

namespace BookStore.App.Infrastructure.Mapping.Models;

public class BookVm : IHaveCustomMapping
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Author { get; set; } = null!;
    public LocalDate ReleaseDate { get; set; }
    public decimal Price { get; set; }

    
    public void CreateMappings(Profile configuration)
    {
        configuration
            .CreateMap<Book, BookVm>()
            .ReverseMap();
    }
}