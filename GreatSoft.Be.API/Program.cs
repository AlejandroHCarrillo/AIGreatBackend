using GreatSoft.Be.Application;
using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Infrastructure;
using GreatSoft.Be.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "GreatSoft API",
        Version = "v1",
        Description = "API para gestión de comunidades residenciales"
    });

    // Add JWT authentication to Swagger
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Add Application and Infrastructure layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Configure CORS
builder.Services.AddCors(options =>
{
    // Policy para desarrollo - permite múltiples orígenes comunes
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:4200", 
                "https://localhost:4200",
                "http://localhost:3000",
                "http://127.0.0.1:4200",
                "http://127.0.0.1:3000",
                "http://localhost:5173", // Vite
                "http://localhost:5174"  // Vite alternativo
              )
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
    
    // Policy más permisiva para desarrollo
    // Permite cualquier origen localhost o 127.0.0.1 en cualquier puerto
    if (builder.Environment.IsDevelopment())
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.SetIsOriginAllowed(origin =>
            {
                if (string.IsNullOrEmpty(origin))
                    return false;
                    
                try
                {
                    // Permitir cualquier origen que sea localhost o 127.0.0.1
                    var uri = new Uri(origin);
                    return uri.Host == "localhost" || 
                           uri.Host == "127.0.0.1" || 
                           uri.Host == "::1" ||
                           origin.Contains("localhost", StringComparison.OrdinalIgnoreCase) ||
                           origin.Contains("127.0.0.1", StringComparison.OrdinalIgnoreCase);
                }
                catch
                {
                    // Si no se puede parsear el URI, permitir si contiene localhost
                    return origin.Contains("localhost", StringComparison.OrdinalIgnoreCase) ||
                           origin.Contains("127.0.0.1", StringComparison.OrdinalIgnoreCase);
                }
            })
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .SetPreflightMaxAge(TimeSpan.FromSeconds(3600)); // Cache preflight por 1 hora
        });
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline
// IMPORTANTE: CORS debe estar ANTES de UseHttpsRedirection y UseAuthentication

// Aplicar CORS primero
if (app.Environment.IsDevelopment())
{
    // En desarrollo, usar política más permisiva
    app.UseCors("AllowAll");
    
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GreatSoft.Be API V1");
        c.RoutePrefix = "swagger"; // Set Swagger UI at /swagger
    });
}
else
{
    // En producción, usar política específica
    app.UseCors("AllowFrontend");
}

// En desarrollo, NO redirigir a HTTPS para evitar problemas con CORS
// En producción, sí redirigir
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var passwordService = services.GetRequiredService<IPasswordService>();
        
        // Seed basic data (roles and admin users)
        await DbSeeder.SeedAsync(context, passwordService);
        
        // Seed dummy data (companies, communities, managers, residents, vehicles, pets)
        await DummyDataSeeder.SeedDummyDataAsync(context, passwordService);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();

