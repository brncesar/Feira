namespace FeirasLivres.Domain.Entities.FeiraEntity.FindFeiraUseCase;

public record FindFeiraParams(
    string? Nome,
    string? Bairro,
    string? Regiao5,
    string? CodDistrito);
