
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

            var connectionString = configuration.GetConnectionString("Npgsql");

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
