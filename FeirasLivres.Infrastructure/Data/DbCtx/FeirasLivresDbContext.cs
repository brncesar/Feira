using FeirasLivres.Domain.Entities.DistritoEntity;
using FeirasLivres.Domain.Entities.FeiraEntity;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FeirasLivres.Infrastructure.Data.DbCtx;

public class FeirasLivresDbContext : DbContext
{
    public FeirasLivresDbContext(DbContextOptions<FeirasLivresDbContext> options) : base(options) {}

    public DbSet<Feira        > Feira         { get; set; } = null!;
    public DbSet<Distrito     > Distrito      { get; set; } = null!;
    public DbSet<SubPrefeitura> SubPrefeitura { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        #if DEBUG
        optionsBuilder.LogTo(Console.WriteLine).EnableSensitiveDataLogging();
        #endif

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .RemovePluralizeConvention()
            .RemoveOneToManyCascadeConvention()
            .ApplyEntitiesTypeConfigurations<FeirasLivresDbContext>(); // DbMaps

        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>().HaveColumnType("varchar").HaveMaxLength(100);

        base.ConfigureConventions(configurationBuilder);
    }
}
