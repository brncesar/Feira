using FeirasLivres.Domain.Entities.Enums;

namespace FeirasLivres.Domain.Entities.FeiraEntity.AddNewFeiraUseCase
{
    public record EditExistingFeiraParams(
        string  Nome,
        string  NumeroRegistro,
        string  SetorCensitarioIBGE,
        string  AreaDePonderacaoIBGE,
        string  CodDistrito,
        string  CodSubPrefeitura,
        Regiao5 Regiao5,
        Regiao8 Regiao8,
        string  EnderecoLogradouro,
        string? EnderecoNumero,
        string  EnderecoBairro,
        string? EnderecoReferencia,
        double  Latitude,
        double  Longitude);
}
