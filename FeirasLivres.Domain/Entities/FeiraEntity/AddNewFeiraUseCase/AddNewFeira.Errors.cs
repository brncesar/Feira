using ErrorOr;
using FeirasLivres.Domain.Common;

namespace FeirasLivres.Domain.Entities.FeiraEntity.AddNewFeiraUseCase
{
    public sealed class AddNewFeiraErrors
    {
        public static Error DuplicateFeira(string? description) => ErrorHelpers.GetError(ErrorType.Conflict, description ?? "Feira already exists");

        public static Error DistritoNotFound() => ErrorHelpers.GetError(ErrorType.NotFound, "The related Distrito informed was not found");

        public static Error SubPrefeituraNotFound() => ErrorHelpers.GetError(ErrorType.NotFound, "The related SupPrefeitura informed was not found");

        public static Error RepositoryError(string description) => ErrorHelpers.GetError(ErrorType.Unexpected, description);
    }
}