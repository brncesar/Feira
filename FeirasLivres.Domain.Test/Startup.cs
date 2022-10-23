using FeirasLivres.Domain.Entities.DistritoEntity;
using FeirasLivres.Domain.Entities.FeiraEntity;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity;
using FeirasLivres.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace FeirasLivres.Domain.Test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFeiraRepository        , FeiraRepositoryMemory        >();
            services.AddTransient<IDistritoRepository     , DistritoRepositoryMemory     >();
            services.AddTransient<ISubPrefeituraRepository, SubPrefeituraRepositoryMemory>();
        }
    }
}
