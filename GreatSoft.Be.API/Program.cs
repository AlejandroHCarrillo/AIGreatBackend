using GreatSoft.Be.Infrastructure;
using GreatSoft.Be.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger with JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GreatSoft.Be API",
        Version = "v1",
        Description = "API for GreatSoft Backend with JWT Authentication"
    });

    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Add Infrastructure services
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GreatSoft.Be API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Seed initial data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedDataAsync(services);
}

app.Run();

static async Task SeedDataAsync(IServiceProvider services)
{
    var context = services.GetRequiredService<ApplicationDbContext>();
    var passwordService = services.GetRequiredService<GreatSoft.Be.Application.Interfaces.IPasswordService>();

    // Check if roles already exist
    if (context.Roles.Any())
    {
        return;
    }

    // Create roles
    var roles = new[]
    {
        new GreatSoft.Be.Domain.Entities.Role
        {
            Id = Guid.NewGuid(),
            Name = "Admin",
            Description = "Administrator role with full access",
            RoleType = "Admin",
            CreatedAt = DateTime.UtcNow
        },
        new GreatSoft.Be.Domain.Entities.Role
        {
            Id = Guid.NewGuid(),
            Name = "SysAdmin",
            Description = "System Administrator role",
            RoleType = "SysAdmin",
            CreatedAt = DateTime.UtcNow
        },
        new GreatSoft.Be.Domain.Entities.Role
        {
            Id = Guid.NewGuid(),
            Name = "Manager",
            Description = "Manager role",
            RoleType = "Manager",
            CreatedAt = DateTime.UtcNow
        },
        new GreatSoft.Be.Domain.Entities.Role
        {
            Id = Guid.NewGuid(),
            Name = "Resident",
            Description = "Resident role",
            RoleType = "Resident",
            CreatedAt = DateTime.UtcNow
        },
        new GreatSoft.Be.Domain.Entities.Role
        {
            Id = Guid.NewGuid(),
            Name = "ResidentPower",
            Description = "Resident Power role",
            RoleType = "ResidentPower",
            CreatedAt = DateTime.UtcNow
        },
        new GreatSoft.Be.Domain.Entities.Role
        {
            Id = Guid.NewGuid(),
            Name = "Vigilance",
            Description = "Vigilance role",
            RoleType = "Vigilance",
            CreatedAt = DateTime.UtcNow
        },
        new GreatSoft.Be.Domain.Entities.Role
        {
            Id = Guid.NewGuid(),
            Name = "Supervision",
            Description = "Supervision role",
            RoleType = "Supervision",
            CreatedAt = DateTime.UtcNow
        }
    };

    context.Roles.AddRange(roles);
    await context.SaveChangesAsync();

    // Get Admin role
    var adminRole = roles.First(r => r.Name == "Admin");

    // Create admin user
    var adminUser = new GreatSoft.Be.Domain.Entities.User
    {
        Id = Guid.NewGuid(),
        FirstName = "Admin",
        LastName = "User",
        Username = "elgrandeahc",
        Email = "admin@greatsoft.com",
        PasswordHash = passwordService.HashPassword("ahc123"),
        IsActive = true,
        CreatedAt = DateTime.UtcNow,
        RoleId = adminRole.Id
    };

    context.Users.Add(adminUser);
    await context.SaveChangesAsync();
}
