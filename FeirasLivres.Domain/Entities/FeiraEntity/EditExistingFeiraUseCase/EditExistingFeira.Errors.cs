using FeirasLivres.Domain.Common;

namespace FeirasLivres.Domain.Entities.FeiraEntity.EditExistingUseCase;

public sealed class EditExistingFeiraErrors
{
    private static string className = nameof(EditExistingFeiraErrors).Replace("Errors", "");

    public static Error FeiraNotFound(string numeroRegistro) => ErrorHelpers.GetError(
        ErrorType.NotFound,
        $"Não existe uma feira cadastrada com o reigistro {numeroRegistro}. Não foi possível atualizar as informações",
        "EditExistingFeira.Execute");
}