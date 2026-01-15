namespace Ravm.Infrastructure.Persistence.EntityFramework.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Ravm.Domain.Common;

/// <summary>
/// Базовый класс конфигурации для сущностей поддерживающих локализации
/// </summary>
/// <typeparam name="TEntity">Класс производный от LocalizableEntity</typeparam>
public abstract class LocalizableEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : LocalizableEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(a => a.Name).HasMaxLength(256);
        builder.Property(a => a.NameRu).HasMaxLength(256);
        builder.Property(a => a.NameUz).HasMaxLength(256);
        builder.Property(a => a.NameKa).HasMaxLength(256);
    }
}
