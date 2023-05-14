using FeirasLivres.Domain.Entities.Common;

namespace FeirasLivres.Domain.Entities.SubPrefeituraEntity.FindSubPrefeituraUseCase;

public interface IFindSubPrefeitura : IUseCase<FindSubPrefeituraParams, List<FindSubPrefeituraResult>>
{
}