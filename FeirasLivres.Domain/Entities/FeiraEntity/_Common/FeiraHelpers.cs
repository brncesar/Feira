using FeirasLivres.Domain.Misc;

namespace FeirasLivres.Domain.Entities.FeiraEntity.Common;

public static class FeiraHelpers
{
    public static FeiraResult ToFeiraResult(this Feira feira) => new FeiraResult(
        feira.Nome,
        feira.NumeroRegistro,
        feira.SetorCensitarioIBGE,
        feira.AreaDePonderacaoIBGE,
        CodDistrito: feira.Distrito.Codigo,
        Distrito: feira.Distrito.Nome,
        CodSubPrefeitura: feira.SubPrefeitura.Codigo,
        SubPrefeitura: feira.SubPrefeitura.Nome,
        Regiao5: feira.Regiao5.ToDescription(),
        Regiao8: feira.Regiao8.ToDescription(),
        feira.EnderecoLogradouro,
        feira.EnderecoNumero,
        feira.EnderecoBairro,
        feira.EnderecoReferencia,
        feira.Latitude,
        feira.Longitude);
}
