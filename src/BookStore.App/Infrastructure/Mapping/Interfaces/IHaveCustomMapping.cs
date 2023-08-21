using AutoMapper;

namespace BookStore.App.Infrastructure.Mapping.Interfaces;

public interface IHaveCustomMapping
{
    void CreateMappings(Profile configuration);
}