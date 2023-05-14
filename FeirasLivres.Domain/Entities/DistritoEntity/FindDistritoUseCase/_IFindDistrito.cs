using FeirasLivres.Domain.Entities.Common;

namespace FeirasLivres.Domain.Entities.DistritoEntity.FindDistritoUseCase;

public interface IFindDistrito : IUseCase<FindDistritoParams, List<FindDistritoResult>>
{
}