using System.Reflection;
using BookStore.App.Infrastructure.Mapping.Infrastructure;

namespace BookStore.App.Infrastructure.Mapping;

public class AutoMapperProfile : BaseAutoMapperProfile
{
    protected override Assembly RootAssembly => Assembly.GetExecutingAssembly();
}