using ErrorOr;
using FeirasLivres.Domain.Common;

namespace FeirasLivres.Domain.Entities.DistritoEntity.FindDistritoUseCase;

public sealed class FindDistritoErrors
{
    public static Error DistritoNotFound() => ErrorHelpers.GetError(ErrorType.NotFound, "Distrito not found");

    public static Error RepositoryError(string description) => ErrorHelpers.GetError(ErrorType.Unexpected, description);
}