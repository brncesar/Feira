using FeirasLivres.Domain.Entities.FeiraEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeirasLivres.Infrastructure.Data.DbCtx.DbMap;

internal class FeiraDbMap : IEntityTypeConfiguration<Feira>
{
    public void Configure(EntityTypeBuilder<Feira> builder)
    {
        builder.ToTable("TP01_Feira");
        builder.HasKey  (p => p.Id                  );
        builder.Property(p => p.Id                  ).HasColumnName("TP01_id_Feira"          ).HasColumnOrder(1);
        builder.Property(p => p.Nome                ).HasColumnName("no_Feira"               ).HasMaxLength(30).IsRequired().HasColumnType("varchar");
        builder.Property(p => p.NumeroRegistro      ).HasColumnName("nu_Registro"            ).HasMaxLength( 6).IsRequired().IsFixedLength().HasColumnType("char");
        builder.Property(p => p.SetorCensitarioIBGE ).HasColumnName("cd_SetorCensitarioIBGE" ).HasMaxLength(15).IsRequired().IsFixedLength().HasColumnType("char");
        builder.Property(p => p.AreaDePonderacaoIBGE).HasColumnName("cd_AreaDePonderacaoIBGE").HasMaxLength(13).IsRequired().IsFixedLength().HasColumnType("char");
        builder.Property(p => p.DistritoId          ).HasColumnName("TS01_id_Distrito"       );
        builder.Property(p => p.SubPrefeituraId     ).HasColumnName("TS02_id_SubPrefeitura"  );
        builder.Property(p => p.Regiao5             ).HasColumnName("cd_Regiao5"             ).HasComment("1: Norte | 2: Leste | 3: Sul | 4: Oeste | 5: Centro");
        builder.Property(p => p.Regiao8             ).HasColumnName("cd_Regiao8"             ).HasComment("11: Norte1 | 12: Norte2 | 21: Leste1 | 22: Leste2 | 31: Sul1 | 32: Sul2 | 4: Oeste | 5: Centro");
        builder.Property(p => p.EnderecoLogradouro  ).HasColumnName("tx_EnderecoLogradouro"  ).HasMaxLength(34).IsRequired().HasColumnType("varchar");
        builder.Property(p => p.EnderecoNumero      ).HasColumnName("tx_EnderecoNumero"      ).HasMaxLength( 5).HasColumnType("varchar");
        builder.Property(p => p.EnderecoBairro      ).HasColumnName("tx_EnderecoBairro"      ).HasMaxLength(20).HasColumnType("varchar");
        builder.Property(p => p.EnderecoReferencia  ).HasColumnName("tx_EnderecoReferencia"  ).HasMaxLength(24).HasColumnType("varchar");
        builder.Property(p => p.Latitude            ).HasColumnName("nu_Latitude"            );
        builder.Property(p => p.Longitude           ).HasColumnName("nu_Longitude"           );

        builder.HasIndex(b => b.NumeroRegistro).IsUnique(true);
    }
}
