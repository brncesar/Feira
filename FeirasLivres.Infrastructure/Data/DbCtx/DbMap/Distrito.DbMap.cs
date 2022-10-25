using FeirasLivres.Domain.Entities.DistritoEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeirasLivres.Infrastructure.Data.DbCtx.DbMap;

internal class DistritoDbMap : IEntityTypeConfiguration<Distrito>
{
    public void Configure(EntityTypeBuilder<Distrito> builder)
    {
        builder.ToTable("TS01_Distrito");
        builder.HasKey  (p => p.Id     );
        builder.Property(p => p.Id     ).HasColumnName("TS01_id_Distrito");
        builder.Property(p => p.Nome   ).HasColumnName("no_Distrito"     ).HasMaxLength(25).IsRequired().HasColumnType("varchar");
        builder.Property(p => p.Codigo ).HasColumnName("cd_Distrito"     ).HasMaxLength( 2).IsRequired().HasColumnType("char").IsFixedLength();
        builder.HasIndex(b => b.Codigo).IsUnique(true);
    }
}
