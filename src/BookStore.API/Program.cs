using System.Reflection;
using BookStore.API.Infrastructure;
using BookStore.API.Infrastructure.Filters;
using BookStore.App.Infrastructure.Interfaces;
using BookStore.App.Infrastructure.Mapping;
using BookStore.App.Infrastructure.Validators;
using BookStore.App.Services;
using BookStore.Persistence;
using BookStore.Persistence.Infrastructure;
using LiveSlotsSpb.API.Infrastructure.Filters;
using NodaTime;

var builder = WebApplication.CreateBuilder(args);
var appAssembly = typeof(AutoMapperProfile).GetTypeInfo().Assembly;
var pathBase = builder.Configuration["PATH_BASE"];
// Add services to the container.

builder.Services
    .AddProjectDbContexts(builder.Configuration)
    .AddCustomSwagger(builder.Configuration)
    .AddAutoMapper(appAssembly)
    .AddRouting(o => { o.LowercaseUrls = true; })
    .AddSingleton<IClock>(sp => SystemClock.Instance)
    .AddHttpContextAccessor()
    .AddCustomCors()
    .AddCustomValidation(appAssembly);


builder.Services
    .AddControllers(o =>
    {

        o.Filters.Add<ModelBindingErrorFilter>();
        o.Filters.Add<OperationCancelledExceptionFilter>();
        o.Filters.Add<CustomExceptionFilter>();
    })
    .AddCustomJsonOptions(builder.Environment);;

builder.Services
    .AddScoped<IBookRepository, BookRepository>()
    .AddScoped<IOrderRepository, OrderRepository>()
    .AddScoped<IBookService, BookService>()
    .AddScoped<IOrderService, OrderService>()
    .AddScoped(typeof(IValidationHelper<>), typeof(ValidationHelper<>));    ;


var app = builder.Build();

app.Lifetime.ApplicationStarted.Register(app.DatabaseMigrate);

app.UseForwardedHeaders();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app
    .MapControllers()
    .RequireCors(CorsPolicy.DEFAULT);

app.Run();