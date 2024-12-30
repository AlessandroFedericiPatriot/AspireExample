using AspireExample.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;
using Xunit.DependencyInjection.Logging; // For xunit v2 - not required for xunit v3

namespace AspireExample.UnitTests;

// https://github.com/pengweiqhca/Xunit.DependencyInjection
// https://community.panoramicdata.com/t/dependency-injection-in-xunit-including-logging/44

public class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("../../../appsettings.json", true)
            .AddEnvironmentVariables()
            .AddUserSecrets<Startup>()
            .Build();

        services.AddLogging((builder) => builder
            .AddDebug()
            .AddFilter(level => level >= LogLevel.Debug)
            .AddXunitOutput());

        services.AddApplication();
    }    
}