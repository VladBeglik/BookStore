using NodaTime;
using NodaTime.Serialization.SystemTextJson;

namespace BookStore.API.Infrastructure;

public static class JsonExtensions
{
    /// <summary>
    /// Adds customized JSON serializer settings.
    /// </summary>
    public static IMvcBuilder AddCustomJsonOptions(
        this IMvcBuilder builder,
        IWebHostEnvironment hostingEnvironment) =>
        builder.AddJsonOptions(
            options =>
            {
                options.JsonSerializerOptions.WriteIndented = hostingEnvironment.IsDevelopment();
                options.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
            });
}