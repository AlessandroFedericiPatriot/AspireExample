using AspireExample.ApiService;
using AspireExample.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Patriot Software
builder.Services
    .AddApplication()
    .AddInfrastructure();

builder.AddNpgsqlDbContext<AspireExampleDbContext>(connectionName: "postgresdb");

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    await SeedDataAsync(app);
}

// Endpoints
app.MapWeatherApiEndpoints();
app.MapFilesApiEndpoints();

app.MapDefaultEndpoints();

// Run
app.Run();

static Task SeedDataAsync(IHost host)
{
    using var scope = host.Services.CreateScope();
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<AspireExampleDbContext>();

    // Rebuilds the DB
    //context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    // Bogus
    //var options = new DatabaseSeeder.GenerationOptions(
    //    DbContext: context,
    //    BlogsCount: 35,
    //    PostsCount: 120,
    //    SourcesCount: 90);
    //
    //await DatabaseSeeder.GenerateAsync(options);

    return Task.CompletedTask;
}