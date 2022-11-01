using FeirasLivres.Domain.Entities.DistritoEntity;
using FeirasLivres.Domain.Entities.FeiraEntity;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity;
using FeirasLivres.Infrastructure.FakeInMemory.Data;
using Microsoft.Extensions.DependencyInjection;

namespace FeirasLivres.Infrastructure.FakeInMemory;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureFakeInMemory(this IServiceCollection services)
    {
        services.AddScoped<IFeiraRepository        , FeiraRepositoryMemory        >();
        services.AddScoped<IDistritoRepository     , DistritoRepositoryMemory     >();
        services.AddScoped<ISubPrefeituraRepository, SubPrefeituraRepositoryMemory>();

        return services;
    }
}
