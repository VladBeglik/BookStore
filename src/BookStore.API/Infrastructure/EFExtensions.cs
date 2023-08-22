
using System.Security.Cryptography;
using BookStore.Domain;
using BookStore.Persistence;
using BookStore.Persistence.Infrastructure;
using EntityFrameworkCore.SqlServer.NodaTime.Extensions;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace BookStore.API.Infrastructure
{
    public static class EFExtensions
    {
        public static IServiceCollection AddProjectDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            var migrationsAssembly = typeof(BookStoreDbContext).Assembly;

            var connectionString = configuration.GetConnectionString("SqlServer");

            services.AddDbContext<BookStoreDbContext>(options =>
            {
                options.UseSqlServer(connectionString, o =>
                {
                    o.MigrationsAssembly(migrationsAssembly.GetName().Name);
                    o.EnableRetryOnFailure(15);
                    o.UseNodaTime();
                });

                if (!LogSqlToConsole) return;

                //options.EnableSensitiveDataLogging();
                //options.UseLoggerFactory(GetConsoleLoggerFactory());
            }
            );

            services.AddScoped<IBookStoreDbContext, BookStoreDbContext>();

            return services;
        }

        public static void DatabaseMigrate(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<BookStoreDbContext>();
            var clock = scope.ServiceProvider.GetRequiredService<IClock>();
            context.Database.Migrate();
            Seed(context);

        }

        private static async void Seed(IBookStoreDbContext ctx)
        {
            if (ctx.Books.Any())
                return;

            await ctx.Books.AddRangeAsync(Books());
            await ctx.SaveChangesAsync();

        }

        private static IEnumerable<Book> Books()
        {
            yield return new Book
            {
                Name = "war",
                Author = "Ivanov",
                Price = 1,
                ReleaseDate = new LocalDate(2020, 2, 14)
            };
            yield return new Book
            {
                Name = "rose",
                Author = "Petrov",
                Price = 1,
                ReleaseDate = new LocalDate(1999, 5, 1)
            };
            yield return new Book
            {
                Name = "war 2",
                Author = "Sidirov",
                Price = 1,
                ReleaseDate = new LocalDate(2020, 2, 14)
            };
            yield return new Book
            {
                Name = "Hi",
                Author = "Nik",
                Price = 1,
                ReleaseDate = new LocalDate(2022, 2, 16)
            };
            yield return new Book
            {
                Name = "war 3",
                Author = "Ivanov",
                Price = 1,
                ReleaseDate = new LocalDate(2020, 2, 14)
            };
            yield return new Book
            {
                Name = "war 4",
                Author = "Ivanov",
                Price = 1,
                ReleaseDate = new LocalDate(2020, 2, 14)
            };
        }
        

        #region private methods
        private static ILoggerFactory GetConsoleLoggerFactory()
        {
            return LoggerFactory.Create(builder =>
            {
                builder.AddConsole()
                    .AddFilter(level => level >= LogLevel.Warning);
            });
        }
        private static bool LogSqlToConsole = false;

        #endregion
    }
}
