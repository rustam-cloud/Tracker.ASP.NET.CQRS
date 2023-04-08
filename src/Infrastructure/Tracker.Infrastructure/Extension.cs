using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tracker.Infrastructure.Dal;
using Tracker.Infrastructure.Dal.Initializers;

namespace Tracker.Infrastructure;

public static class Extension
{
    public static IServiceCollection AddDataBaseContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<TrackerDbContext>(options =>
            options.UseNpgsql(connectionString, b => b.MigrationsAssembly("Tracker.WebApi")));

        services.AddHostedService<DatabaseInitializer<TrackerDbContext>>();
        services.AddHostedService<DataInitializer>();

        return services;
    }

    public static IServiceCollection AddInitializer<T>(this IServiceCollection services) where T : class, IDataInitializer
         => services.AddTransient<IDataInitializer, T>();
}