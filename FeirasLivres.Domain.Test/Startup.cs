using FeirasLivres.Infrastructure.FakeInMemory;
using Microsoft.Extensions.DependencyInjection;

namespace FeirasLivres.Domain.Test;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDomain();
        services.AddInfrastructureFakeInMemory();
    }
}
