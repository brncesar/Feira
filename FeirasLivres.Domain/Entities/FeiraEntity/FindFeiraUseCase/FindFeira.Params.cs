using FeirasLivres.Domain.Entities.Enums;

namespace FeirasLivres.Domain.Entities.FeiraEntity.FindFeiraUseCase;

public record FindFeiraParams(
    string?  Nome,
    string?  Bairro,
    Regiao5? Regiao5,
    string?  CodDistrito);
