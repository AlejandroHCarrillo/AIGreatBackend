using System.Text;
using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Application.Services;
using GreatSoft.Be.Domain.Entities;
using GreatSoft.Be.Infrastructure.Data;
using GreatSoft.Be.Infrastructure.Repositories;
using GreatSoft.Be.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace GreatSoft.Be.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Database - Using InMemory for now, can be changed to SQL Server
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        if (!string.IsNullOrEmpty(connectionString))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
        }
        else
        {
            // Fallback to InMemory if no connection string is provided
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("GreatSoftDb"));
        }

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ICompanyUserRepository, CompanyUserRepository>();
        services.AddScoped<ICommunityRepository, CommunityRepository>();
        services.AddScoped<IRepository<CommunityType>, Repository<CommunityType>>();
        services.AddScoped<IResidentVisitRepository, ResidentVisitRepository>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<IResidentProviderRepository, ResidentProviderRepository>();
        services.AddScoped<IRepository<Resident>, Repository<Resident>>();
        services.AddScoped<IRepository<VehicleType>, Repository<VehicleType>>();
        services.AddScoped<IRepository<ProviderServiceType>, Repository<ProviderServiceType>>();

        // Services
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IJwtService, JwtService>();

        // Application Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ICommunityService, CommunityService>();
        services.AddScoped<IResidentVisitService, ResidentVisitService>();
        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<IPetService, PetService>();
        services.AddScoped<IResidentProviderService, ResidentProviderService>();

        // JWT Authentication
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"] ?? "YourSuperSecretKeyThatShouldBeAtLeast32CharactersLong!";
        var issuer = jwtSettings["Issuer"] ?? "GreatSoft.Be";
        var audience = jwtSettings["Audience"] ?? "GreatSoft.Be";

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddAuthorization();

        return services;
    }
}

