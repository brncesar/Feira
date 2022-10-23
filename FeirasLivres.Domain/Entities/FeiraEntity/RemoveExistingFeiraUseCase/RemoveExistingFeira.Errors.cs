using ErrorOr;
using FeirasLivres.Domain.Common;

namespace FeirasLivres.Domain.Entities.FeiraEntity.RemoveExistingFeiraUseCase
{
    public sealed class RemoveExistingFeiraErrors
    {
        public static Error FeiraDoesntExistis(string? description) => ErrorHelpers.GetError(ErrorType.NotFound, description ?? "Feira doesn't exists");

        public static Error RepositoryError(string description) => ErrorHelpers.GetError(ErrorType.Unexpected, description);
    }
}