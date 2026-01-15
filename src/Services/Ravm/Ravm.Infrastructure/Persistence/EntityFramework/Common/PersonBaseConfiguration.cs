namespace Ravm.Infrastructure.Persistence.EntityFramework.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Ravm.Domain.Common;

/// <summary>
/// Базовый класс конфигурации для сущностей людей
/// </summary>
/// <typeparam name="TEntity">Класс производный от PersonBase</typeparam>
public abstract class PersonBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : PersonBase
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasIndex(e => e.Id);
        builder.Property(a => a.FirstName).HasMaxLength(256);
        builder.Property(a => a.LastName).HasMaxLength(256);
        builder.Property(a => a.MiddleName).HasMaxLength(256);
        builder.Property(s => s.FullName)
            .HasComputedColumnSql("Trim(\"LastName\" || ' ' || \"FirstName\" || ' ' || coalesce(\"MiddleName\", ''))", stored: true);
    }
}
