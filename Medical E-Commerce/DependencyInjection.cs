using Medical_E_Commerce.Entities;
using Medical_E_Commerce.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Medical_E_Commerce;

public static class DependencyInjection
{
    public static IServiceCollection AddDependency(this IServiceCollection services , IConfiguration configuration)
    {
        services.AddControllers();
        
        services
            .AddSwagger()
            .AddDatabase(configuration)
            .AddAuth();
        return services;
    }

    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddOpenApi();
        services.AddSwaggerGen();

        return services;
    }
    
    private static IServiceCollection AddDatabase(this IServiceCollection services , IConfiguration configuration)
    {
        var ConnectionString = configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string is not found in the configuration file");

        services.AddDbContext<ApplicationDbcontext>(options =>
            options.UseSqlServer(ConnectionString));

        return services;

    }
    private static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, ApplicationRoles>()
            .AddEntityFrameworkStores<ApplicationDbcontext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
