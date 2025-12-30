using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GreatSoft.Be.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Application Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ICommunityService, CommunityService>();
        services.AddScoped<IPetService, PetService>();
        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<IResidentProviderService, ResidentProviderService>();
        services.AddScoped<IResidentVisitService, ResidentVisitService>();
        services.AddScoped<IAmenityService, AmenityService>();
        services.AddScoped<IResidentPreferenceService, ResidentPreferenceService>();

        return services;
    }
}

