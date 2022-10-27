namespace FeirasLivres.Domain.Entities.FeiraEntity.AddNewFeiraUseCase
{
    public record AddNewFeiraParams(
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
