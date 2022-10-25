using ErrorOr;
using FeirasLivres.Domain.Common;

namespace FeirasLivres.Domain.Entities.FeiraEntity.FindDistritoUseCase;

public sealed class FindDistritoErrors
{
    public static Error RepositoryError(string description) => ErrorHelpers.GetError(ErrorType.Unexpected, description);
}