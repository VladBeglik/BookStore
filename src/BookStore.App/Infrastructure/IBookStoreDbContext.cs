using BookStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookStore.App.Infrastructure;

public interface IBookStoreDbContext
{ 
    public DbSet<Book> Books { get; set; }
    public DbSet<Order> Orders { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken token = default);
    IExecutionStrategy CreateExecutionStrategy();
}