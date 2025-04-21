
using StackExchange.Redis;

namespace Persistence;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StoreDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IDbInitializer, DbInitializer>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddSingleton<IConnectionMultiplexer>((_) => 
        {
            return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")!);
        });
        return services;
    }
}
