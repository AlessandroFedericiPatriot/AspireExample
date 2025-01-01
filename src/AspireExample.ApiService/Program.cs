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
    var httpCtx = sp.GetService<HttpContextAccessor>();
    
    // TODO: temporary. Add Auth
    var defaultClaims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, "John Doe"),
        new Claim(ClaimTypes.NameIdentifier, "123"),
        new Claim(ClaimTypes.Email, "") 
    };
    var principal = new ClaimsPrincipal(new ClaimsIdentity(defaultClaims));    

    return new UserContext(httpCtx?.HttpContext?.User ?? principal);
});


builder.Services
    .AddApplication()
    .AddInfrastructure();

builder.Services.AddScoped<ISaveChangesInterceptor, TrackedEntityInterceptor>();
builder.Services.AddDbContext<AspireExampleDbContext>(
    (sp, options) =>
    {
        var connStr = builder.Configuration.GetConnectionString("postgres-files");
        options.UseNpgsql(connStr);
        options.AddInterceptors(sp.GetRequiredService<ISaveChangesInterceptor>());
    });

builder.EnrichNpgsqlDbContext<AspireExampleDbContext>();

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

