using FeirasLivres.Domain.Entities.DistritoEntity.FindDistritoUseCase;
using FeirasLivres.Domain.Entities.DistritoEntity.GetDistritoByCodigoUseCase;
using FeirasLivres.Domain.Entities.FeiraEntity.AddNewFeiraUseCase;
using FeirasLivres.Domain.Entities.FeiraEntity.EditExistingFeiraUseCase;
using FeirasLivres.Domain.Entities.FeiraEntity.FindFeiraUseCase;
using FeirasLivres.Domain.Entities.FeiraEntity.RemoveExistingFeiraUseCase;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity.FindSubPrefeituraUseCase;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity.GetSubPrefeituraByCodigoUseCase;
using Microsoft.Extensions.DependencyInjection;

namespace FeirasLivres.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            return services
                .AddScoped<FindDistrito            >()
                .AddScoped<GetDistritoByCodigo     >()
                .AddScoped<FindSubPrefeitura       >()
                .AddScoped<GetSubPrefeituraByCodigo>()
                .AddScoped<FindFeira               >()
                .AddScoped<AddNewFeira             >()
                .AddScoped<RemoveExistingFeira     >()
                .AddScoped<EditExistingFeira       >();
        }
    }
}