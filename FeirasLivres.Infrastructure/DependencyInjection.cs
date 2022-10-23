using Microsoft.Extensions.DependencyInjection;

namespace FeirasLivres.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // services.Services.AddScoped<IAutenticationService, AutenticationService>();

            return services;
        }
    }
}
