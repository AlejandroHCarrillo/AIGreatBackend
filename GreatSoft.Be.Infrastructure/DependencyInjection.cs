using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Infrastructure.Data;
using GreatSoft.Be.Infrastructure.Repositories;
using GreatSoft.Be.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GreatSoft.Be.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Database
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        if (!string.IsNullOrEmpty(connectionString))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, sql => sql.EnableRetryOnFailure()));
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
        services.AddScoped<ICommunityRepository, CommunityRepository>();
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IResidentProviderRepository, ResidentProviderRepository>();
        services.AddScoped<IResidentVisitRepository, ResidentVisitRepository>();
        services.AddScoped<IAmenityRepository, AmenityRepository>();
        services.AddScoped<IResidentPreferenceRepository, ResidentPreferenceRepository>();

        // Infrastructure Services
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IJwtService, JwtService>();

        // JWT Authentication
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"] ?? "YourSuperSecretKeyThatShouldBeAtLeast32CharactersLong!";
        var issuer = jwtSettings["Issuer"] ?? "GreatSoft";
        var audience = jwtSettings["Audience"] ?? "GreatSoftUsers";

        // Ensure secret key is at least 32 characters for HMAC SHA256
        if (string.IsNullOrEmpty(secretKey) || secretKey.Length < 32)
        {
            throw new InvalidOperationException("JWT SecretKey must be at least 32 characters long.");
        }

        var keyBytes = Encoding.UTF8.GetBytes(secretKey);
        var signingKey = new SymmetricSecurityKey(keyBytes);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true
            };
        });

        services.AddAuthorization();

        return services;
    }
}

