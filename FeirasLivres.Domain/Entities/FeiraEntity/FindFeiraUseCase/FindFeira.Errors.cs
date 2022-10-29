using FeirasLivres.Domain.Common;

namespace FeirasLivres.Domain.Entities.FeiraEntity.FindFeiraUseCase;

public sealed class FindFeiraErrors
{
    private static string className = nameof(FindFeiraErrors).Replace("Errors", "");

    public static Error DistritoNotFound() => ErrorHelpers.GetError(ErrorType.NotFound, "The related Distrito informed was not found"/*, className, "DistritoNotFound"*/);
}