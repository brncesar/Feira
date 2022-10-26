using FeirasLivres.Domain.Entities.DistritoEntity.FindDistritoUseCase;
using FeirasLivres.Domain.Entities.DistritoEntity.GetDistritoByCodigoUseCase;
using Microsoft.Extensions.DependencyInjection;

namespace FeirasLivres.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddScoped<FindDistrito>();
            services.AddScoped<GetDistritoByCodigo>();

            return services;
        }
    }
}