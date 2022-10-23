using FeirasLivres.Domain.Entities.Enums;

namespace FeirasLivres.Domain.Entities.FeiraEntity.AddNewFeiraUseCase
{
    public record AddNewFeiraParams(
        string Nome,
        string NumeroRegistro,
        string SetorCensitarioIBGE,
        string AreaDePonderacaoIBGE,
        Guid DistritoId,
        Guid SubPrefeituraId,
        Regiao5 Regiao5,
        Regiao8 Regiao8,
        string EnderecoLogradouro,
        string? EnderecoNumero,
        string EnderecoBairro,
        string? EnderecoReferencia,
        double Latitude,
        double Longitude);
}
