using FeirasLivres.Domain.Entities.Enums;
using FeirasLivres.Domain.Entities.DistritoEntity;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity;
using FeirasLivres.Domain.Entities.Common;

namespace FeirasLivres.Domain.Entities.FeiraEntity;

public class Feira : BaseEntity
{
    public string        Nome                 { get; set; } = null!;
    public string        NumeroRegistro       { get; set; } = null!;
    public string        SetorCensitarioIBGE  { get; set; } = null!;
    public string        AreaDePonderacaoIBGE { get; set; } = null!;
    public Guid          DistritoId           { get; set; }
    public Distrito      Distrito             { get; set; } = null!;
    public Guid          SubPrefeituraId      { get; set; }
    public SubPrefeitura SubPrefeitura        { get; set; } = null!;
    public Regiao5       Regiao5              { get; set; }
    public Regiao8       Regiao8              { get; set; }
    public string        EnderecoLogradouro   { get; set; } = null!;
    public string?       EnderecoNumero       { get; set; }
    public string        EnderecoBairro       { get; set; } = null!;
    public string?       EnderecoReferencia   { get; set; }
    public double        Latitude             { get; set; } // min/max »   -90  to  +90
    public double        Longitude            { get; set; } // min/max »  -180  to +180
}