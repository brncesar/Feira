using ErrorOr;
using FeirasLivres.Domain.Common;

namespace FeirasLivres.Domain.Entities.FeiraEntity.AddNewFeiraUseCase;

public sealed class AddNewFeiraErrors
{
    private static string className = nameof(AddNewFeiraErrors).Replace("Errors", "");

    public static Error DuplicateFeira(string? description, string errorCode) => ErrorHelpers.GetError(ErrorType.Conflict, description ?? "Feira already exists", errorCode);

    public static Error DistritoNotFound() => ErrorHelpers.GetError(ErrorType.NotFound, "The related Distrito informed was not found", codeError: "DistritoNotFound");

    public static Error SubPrefeituraNotFound() => ErrorHelpers.GetError(ErrorType.NotFound, "The related SupPrefeitura informed was not found", codeError: "SubPrefeituraNotFound");

    public static Error RepositoryError(string description) => ErrorHelpers.GetError(ErrorType.Unexpected, description/*, className, "RepositoryError"*/);
}