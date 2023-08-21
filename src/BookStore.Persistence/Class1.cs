using System.Reflection;
using BookStore.App.Infrastructure;
using BookStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using NodaTime;

namespace BookStore.Persistence;

public class BookStoreDbContext : DbContext, IBookStoreDbContext
{

    public DbSet<Book> Books { get; set; }
    public DbSet<Order> Orders { get; set; }

    #region Fields

    private readonly IClock _clock;
    private readonly ILogger<BookStoreDbContext> _logger;

    #endregion
    
    #region Ctors

    public BookStoreDbContext(ILogger<BookStoreDbContext> logger, IClock clock)
    {
        _logger = logger;
        _clock = clock;
    }    
    
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options, ILogger<BookStoreDbContext> logger, IClock clock) : base(options)
    {
        _logger = logger;
        _clock = clock;
    }

    #endregion

    #region Methods

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken token = new())
    {
        return Database.BeginTransactionAsync(token);
    }

    public IExecutionStrategy CreateExecutionStrategy()
    {
        return Database.CreateExecutionStrategy();
    }
    
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    #endregion

}