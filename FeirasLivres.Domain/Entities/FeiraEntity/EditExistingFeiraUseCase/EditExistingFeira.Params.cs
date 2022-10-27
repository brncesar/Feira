using FeirasLivres.Domain.Entities.Enums;

namespace FeirasLivres.Domain.Entities.FeiraEntity.EditExistingFeiraUseCase
{
    public record EditExistingFeiraParams(
        string  Nome,
        string  NumeroRegistro,
        string  SetorCensitarioIBGE,
        string  AreaDePonderacaoIBGE,
        string  CodDistrito,
        string  CodSubPrefeitura,
        string  Regiao5,
        string  Regiao8,
        string  EnderecoLogradouro,
        string? EnderecoNumero,
        string  EnderecoBairro,
        string? EnderecoReferencia,
        double  Latitude,
        double  Longitude);
}
