using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using Medical_E_Commerce.Entities;
using Medical_E_Commerce.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Medical_E_Commerce;

public static class DependencyInjection
{
    public static IServiceCollection AddDependency(this IServiceCollection services , IConfiguration configuration)
    {
        services.AddControllers();
        
        services
            .AddSwagger()
            .AddDatabase(configuration)
            .AddAuth()
            .AddCORS()
            .Add_Mapster()
            .AddFluant_Validation();
        return services;
    }

    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddOpenApi();
        services.AddSwaggerGen();

        return services;
    }
    private static IServiceCollection AddFluant_Validation(this IServiceCollection services)
    {
        services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
    private static IServiceCollection Add_Mapster(this IServiceCollection services)
    {
        services.AddMapster();

        return services;
    }
    private static IServiceCollection AddCORS(this IServiceCollection services)
    {
        services.AddCors(
            options =>
            options.AddDefaultPolicy(
               builder =>
               builder
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
                ));
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
