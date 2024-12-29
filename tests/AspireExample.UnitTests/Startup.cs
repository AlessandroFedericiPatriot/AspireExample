using AspireExample.Application;
using Microsoft.Extensions.DependencyInjection;

namespace AspireExample.UnitTests;

// https://github.com/pengweiqhca/Xunit.DependencyInjection

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication();
    }    
}