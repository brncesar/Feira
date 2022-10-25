using ErrorOr;
using FeirasLivres.Domain.Common;

namespace FeirasLivres.Domain.Entities.FeiraEntity.FindFeiraUseCase;

public sealed class FindFeiraErrors
{
    public static Error DistritoNotFound() => ErrorHelpers.GetError(ErrorType.NotFound, "The related Distrito informed was not found");

    public static Error RepositoryError(string description) => ErrorHelpers.GetError(ErrorType.Unexpected, description);
}