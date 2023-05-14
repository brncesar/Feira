using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.FeiraEntity.Common;

namespace FeirasLivres.Domain.Entities.FeiraEntity.FindFeiraUseCase;

public interface IFindFeira : IUseCase<FindFeiraParams, List<FeiraResult>>
{
}