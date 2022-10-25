using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity;

namespace SubPrefeiturasLivres.Infrastructure.Data.DbCtx.DbMap;

internal class SubPrefeituraDbMap : IEntityTypeConfiguration<SubPrefeitura>
{
    public void Configure(EntityTypeBuilder<SubPrefeitura> builder)
    {
        builder.ToTable("TS02_SubPrefeitura");
        builder.HasKey  (p => p.Id    );
        builder.Property(p => p.Id    ).HasColumnName("TS02_id_SubPrefeitura");
        builder.Property(p => p.Nome  ).HasColumnName("no_SubPrefeitura"     ).HasMaxLength(25).IsRequired().HasColumnType("varchar");
        builder.Property(p => p.Codigo).HasColumnName("cd_SubPrefeitura"     ).HasMaxLength( 2).IsRequired().HasColumnType("varchar");

        builder.HasIndex(b => b.Codigo).IsUnique(true);
    }
}
