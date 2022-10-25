using ErrorOr;
using FeirasLivres.Domain.Common;

namespace FeirasLivres.Domain.Entities.SubPrefeituraEntity.FindSubPrefeituraUseCase;

public sealed class FindSubPrefeituraErrors
{
    public static Error SubPrefeituraNotFound() => ErrorHelpers.GetError(ErrorType.NotFound, "SubPrefeitura not found");
    public static Error RepositoryError(string description) => ErrorHelpers.GetError(ErrorType.Unexpected, description);
}