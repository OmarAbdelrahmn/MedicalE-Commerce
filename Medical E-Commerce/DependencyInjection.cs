using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Medical_E_Commerce.Authentication;
using Medical_E_Commerce.Entities;
using Medical_E_Commerce.Persistence;
using Medical_E_Commerce.Service.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Medical_E_Commerce;

public static class DependencyInjection
{
    public static IServiceCollection AddDependency(this IServiceCollection services , IConfiguration configuration)
    {
        services.AddControllers();
        
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IAuthService, AuthService>();

        services
            .AddSwagger()
            .AddDatabase(configuration)
            .AddAuth(configuration)
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
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMapper>(new Mapper(mappingConfig));

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
    private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {      

        services.AddIdentity<ApplicationUser, ApplicationRoles>()
            .AddEntityFrameworkStores<ApplicationDbcontext>()
            .AddDefaultTokenProviders();

        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

        var Jwtsetting = configuration.GetSection("Jwt").Get<JwtOptions>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {


                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = Jwtsetting?.Audience,
                ValidIssuer = Jwtsetting?.Issuer,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Jwtsetting?.Key!))
            };
        });
        services.Configure<IdentityOptions>(options =>
        {
            // Default Lockout settings.
            //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //options.Lockout.MaxFailedAccessAttempts = 5;
            //options.Lockout.AllowedForNewUsers = true;
            options.Password.RequiredLength = 8;
            options.SignIn.RequireConfirmedEmail = true;
            options.User.RequireUniqueEmail = true;


        });


        return services;
    }
}
