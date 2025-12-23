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
        c.RoutePrefix = "swagger"; // Set Swagger UI at /swagger
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Ensure database is created and seed initial data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var passwordService = services.GetRequiredService<GreatSoft.Be.Application.Interfaces.IPasswordService>();
    var configuration = services.GetRequiredService<IConfiguration>();
    
    // Check if database should be recreated
    var recreateDatabase = configuration.GetValue<bool>("DatabaseSettings:RecreateDatabaseOnStartup", false);
    
    if (recreateDatabase)
    {
        // Delete and recreate database
        await DataSeeder.EnsureDatabaseCreatedAsync(context);
    }
    else
    {
        // Ensure database exists (create if not exists, but don't delete existing)
        await context.Database.EnsureCreatedAsync();
    }
    
    // Seed initial data
    await DataSeeder.SeedDataAsync(context, passwordService);
}

app.Run();
