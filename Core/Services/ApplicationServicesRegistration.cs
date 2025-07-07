
namespace Services;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddScoped<IServiceManager, ServiceManager>();
        services.AddAutoMapper(typeof(Services.AssemblyReference));
        services.Configure<JWTOptions>(configuration.GetSection("JWTOptions"));
        return services;
    }
}
