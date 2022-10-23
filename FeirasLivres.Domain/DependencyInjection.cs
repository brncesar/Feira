using Microsoft.Extensions.DependencyInjection;

namespace FeirasLivres.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            // services.Services.AddScoped<IFeiraRepsitory, AutenticationService>();

            return services;
        }
    }
}