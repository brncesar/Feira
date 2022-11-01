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
        builder.Property(p => p.Id     ).HasColumnName("TS01_id_Distrito").HasColumnOrder(1);
        builder.Property(p => p.Nome   ).HasColumnName("no_Distrito"     ).HasMaxLength(18).IsRequired().HasColumnType("varchar");
        builder.Property(p => p.Codigo ).HasColumnName("cd_Distrito"     ).HasMaxLength( 9).IsRequired().HasColumnType("varchar");
        builder.HasIndex(b => b.Codigo).IsUnique(true);
    }
}
