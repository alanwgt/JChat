using System.Reflection;
using JChat.Domain.Interfaces;
using JChat.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace JChat.Infrastructure.Extensions;

public static class ModelBuilderExtensions
{
    private static void ConfigureAuditableEntity<TEntity>(ModelBuilder modelBuilder) where TEntity : class, IAuditableEntity
    {
        modelBuilder.Entity<TEntity>(builder =>
        {
            builder.HasOne(e => e.CreatedBy);
            builder.HasOne(e => e.DeletedBy);
            builder.HasOne(e => e.LastModifiedBy);
        });
    }

    private static void ConfigureEntity<TEntity>(ModelBuilder modelBuilder) where TEntity : Entity
    {
        modelBuilder.Entity<TEntity>(builder =>
        {
            // enable soft delete
            builder.HasQueryFilter(e => e.DeletedAt == null);
        });
    }

    public static ModelBuilder ApplyAuditableEntityConfiguration(this ModelBuilder modelBuilder)
    {
        var configureIAuditable = typeof(ModelBuilderExtensions).GetTypeInfo().DeclaredMethods
            .Single(m => m.Name == nameof(ConfigureAuditableEntity));
        var configureEntity = typeof(ModelBuilderExtensions).GetTypeInfo().DeclaredMethods
            .Single(m => m.Name == nameof(ConfigureEntity));

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.ClrType.IsSubclassOf(typeof(Entity)))
                configureEntity.MakeGenericMethod(entityType.ClrType).Invoke(null, new[] { modelBuilder });

            if (entityType.ClrType.IsSubclassOf(typeof(IAuditableEntity)))
                configureIAuditable.MakeGenericMethod(entityType.ClrType).Invoke(null, new[] { modelBuilder });
        }

        return modelBuilder;
    }
}