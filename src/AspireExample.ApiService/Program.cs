using System.Security.Claims;
using AspireExample.ApiService;
using AspireExample.Application.Interfaces;
using AspireExample.Infrastructure.Data;
using AspireExample.Infrastructure.Data.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Patriot Software
builder.Services.AddScoped<IUserContext>(sp => 
{ 
    var httpCtx = sp.GetService<IHttpContextAccessor>();
    
    // TODO: temporary. Add Auth
    var defaultClaims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, "johndoe@example.com"),
        new Claim(ClaimTypes.NameIdentifier, "John Doe"),
        new Claim(ClaimTypes.Email, "johndoe@example.com") 
    };
    var principal = new ClaimsPrincipal(new ClaimsIdentity(defaultClaims));    

    return new UserContext(httpCtx?.HttpContext?.User ?? principal);
});

var connStr = builder.Configuration.GetConnectionString("postgres-files");

builder.Services
    .AddApplication()
    .AddInfrastructure(connStr!);

builder.EnrichNpgsqlDbContext<AspireExampleDbContext>(); // Add Aspire configuration

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    await AspireExampleDbContextDataSeeder.SeedDataAsync(app);
}

// Endpoints
app.MapWeatherApiEndpoints();
app.MapFilesApiEndpoints();

app.MapDefaultEndpoints();

// Run
app.Run();

