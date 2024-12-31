var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache")
                   .WithLifetime(ContainerLifetime.Persistent);

var postgres = builder.AddPostgres("postgres")
                      .WithDataVolume("postgres-data", isReadOnly: false)
                      .WithLifetime(ContainerLifetime.Persistent)
                      .WithPgAdmin(c =>
                      {
                          // Somehow it always rebuilds. 
                          c.WithLifetime(ContainerLifetime.Persistent);
                      });

var postgresdb = postgres.AddDatabase("postgresdb");

var apiService = builder.AddProject<Projects.AspireExample_ApiService>("apiservice")
                        .WithReference(postgresdb).WaitFor(postgresdb);

builder.AddProject<Projects.AspireExample_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache).WaitFor(cache)
    .WithReference(apiService).WaitFor(apiService);

builder.Build().Run();
