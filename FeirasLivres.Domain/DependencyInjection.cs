using FeirasLivres.Domain.Entities.DistritoEntity.FindDistritoUseCase;
using FeirasLivres.Domain.Entities.DistritoEntity.GetDistritoByCodigoUseCase;
using FeirasLivres.Domain.Entities.FeiraEntity.AddNewFeiraUseCase;
using FeirasLivres.Domain.Entities.FeiraEntity.EditExistingFeiraUseCase;
using FeirasLivres.Domain.Entities.FeiraEntity.FindFeiraUseCase;
using FeirasLivres.Domain.Entities.FeiraEntity.RemoveExistingFeiraUseCase;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity.FindSubPrefeituraUseCase;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity.GetSubPrefeituraByCodigoUseCase;
using Microsoft.Extensions.DependencyInjection;

namespace FeirasLivres.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services
            .AddScoped<IFindDistrito            , FindDistrito            >()
            .AddScoped<IGetDistritoByCodigo     , GetDistritoByCodigo     >()
            .AddScoped<IFindSubPrefeitura       , FindSubPrefeitura       >()
            .AddScoped<IGetSubPrefeituraByCodigo, GetSubPrefeituraByCodigo>()
            .AddScoped<IFindFeira               , FindFeira               >()
            .AddScoped<IAddNewFeira             , AddNewFeira             >()
            .AddScoped<IRemoveExistingFeira     , RemoveExistingFeira     >()
            .AddScoped<IEditExistingFeira       , EditExistingFeira       >();
    }
}