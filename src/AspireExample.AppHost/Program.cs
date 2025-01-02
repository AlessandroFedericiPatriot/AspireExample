using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache")
                   .WithLifetime(ContainerLifetime.Persistent);

//var dbUsername = builder.AddParameter("db-username", secret: true);
//var dbPassword = builder.AddParameter("db-password", secret: true);
//var value = builder.Configuration["Parameters:db-password"];

var postgres = builder.AddPostgres("postgres", port: 13495)//, password: dbPassword)
                      .WithDataVolume("postgres-data", isReadOnly: false)
                      .WithLifetime(ContainerLifetime.Persistent)
                      //.WithPgAdmin()
                      .WithExternalHttpEndpoints()
                      ;

var postgresdb = postgres.AddDatabase("postgres-files");

var apiService = builder.AddProject<Projects.AspireExample_ApiService>("apiservice")
                        .WithReference(postgres).WaitFor(postgres)
                        .WithReference(postgresdb).WaitFor(postgresdb);

builder.AddProject<Projects.AspireExample_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache).WaitFor(cache)
    .WithReference(apiService).WaitFor(apiService);

builder.Build().Run();
