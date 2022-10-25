using Microsoft.EntityFrameworkCore;

namespace FeirasLivres.Infrastructure.Data.DbCtx;

public static class DbContextExtensions
{
    public static ModelBuilder RemovePluralizeConvention(this ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
            entityType.SetTableName(entityType.DisplayName());

        return builder;
    }

    public static ModelBuilder RemoveOneToManyCascadeConvention(this ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
            entityType.GetForeignKeys()
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade).ToList()
                .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);

        return builder;
    }

    public static ModelBuilder SetStringTypeToVarchar100AsDefault(this ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
            foreach (var stringProperty in entityType.GetProperties().Where(p => p.ClrType == typeof(string)))
                if (string.IsNullOrWhiteSpace(stringProperty.GetColumnType()) && stringProperty.GetMaxLength() is null)
                    stringProperty.SetColumnType("VARCHAR(100)");

        return builder;
    }

    public static ModelBuilder ApplyEntitiesTypeConfigurations<TDbContext>(this ModelBuilder builder)
        where TDbContext : DbContext
    {
        return builder.ApplyConfigurationsFromAssembly(typeof(TDbContext).Assembly);
    }
}
