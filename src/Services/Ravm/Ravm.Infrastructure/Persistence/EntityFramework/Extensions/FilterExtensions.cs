namespace Ravm.Infrastructure.Persistence.EntityFramework.Extensions;

using Microsoft.EntityFrameworkCore;
using Ravm.Domain.Common;
using System.Reflection;

public static class FilterExtensions
{
    public static ModelBuilder SetSoftDeleteFilter(this ModelBuilder modelBuilder, Type entityType)
    {
        SetSoftDeleteFilterMethod.MakeGenericMethod(entityType)
            .Invoke(null, [modelBuilder]);

        return modelBuilder;
    }

    private static readonly MethodInfo SetSoftDeleteFilterMethod = typeof(FilterExtensions)
               .GetMethods(BindingFlags.Public | BindingFlags.Static)
               .Single(t => t.IsGenericMethod && t.Name == nameof(SetSoftDeleteFilter));

    public static ModelBuilder SetSoftDeleteFilter<TEntity>(this ModelBuilder modelBuilder)
        where TEntity : class, IDeletable
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter(x => !x.IsDeleted);

        return modelBuilder;
    }

    public static ModelBuilder SetSoftDeleteFilter(this ModelBuilder modelBuilder)
    {
        foreach (var type in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(IDeletable).IsAssignableFrom(type.ClrType))
                modelBuilder.SetSoftDeleteFilter(type.ClrType);
        }

        return modelBuilder;
    }

    public static ModelBuilder ConfigureLocalizableEnitity(this ModelBuilder modelBuilder, Type entityType)
    {
        ConfigureLocalizableEnitityMethod.MakeGenericMethod(entityType)
            .Invoke(null, [modelBuilder]);

        return modelBuilder;
    }

    public static readonly MethodInfo ConfigureLocalizableEnitityMethod = typeof(FilterExtensions)
               .GetMethods(BindingFlags.Public | BindingFlags.Static)
               .Single(t => t.IsGenericMethod && t.Name == nameof(ConfigureLocalizableEnitity));

    public static ModelBuilder ConfigureLocalizableEnitity<TEntity>(this ModelBuilder modelBuilder)
        where TEntity : class, ILocalizableEntity
    {
        var entity = modelBuilder.Entity<TEntity>();

        entity.Property(a => a.Name).HasMaxLength(256);
        entity.Property(a => a.NameRu).HasMaxLength(256);
        entity.Property(a => a.NameUz).HasMaxLength(256);
        entity.Property(a => a.NameKa).HasMaxLength(256);

        return modelBuilder;
    }

    public static ModelBuilder ConfigureLocalizableEntities(this ModelBuilder modelBuilder)
    {
        foreach (var type in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ILocalizableEntity).IsAssignableFrom(type.ClrType))
                modelBuilder.ConfigureLocalizableEnitity(type.ClrType);
        }

        return modelBuilder;
    }
}
