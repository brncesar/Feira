using FeirasLivres.Domain.Entities.DistritoEntity;
using FeirasLivres.Domain.Entities.FeiraEntity;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity;
using FeirasLivres.Infrastructure.Data.DbCtx;
using FeirasLivres.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FeirasLivres.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<FeirasLivresDbContext>();
            services.AddScoped<IFeiraRepository        , FeiraRepository        >();
            services.AddScoped<IDistritoRepository     , DistritoRepository     >();
            services.AddScoped<ISubPrefeituraRepository, SubPrefeituraRepository>();

            services.AddDbContext<FeirasLivresDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("FeirasLivresConnection")));

            return services;
        }
    }
}
