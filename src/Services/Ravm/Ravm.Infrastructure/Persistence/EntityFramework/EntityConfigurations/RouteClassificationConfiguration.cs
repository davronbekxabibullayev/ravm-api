namespace Ravm.Infrastructure.Persistence.EntityFramework.EntityConfigurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ravm.Domain.Models;
using Ravm.Infrastructure.Persistence.EntityFramework.Common;

public class RouteClassificationConfiguration : LocalizableEntityConfiguration<RouteClassification>
{
    public override void Configure(EntityTypeBuilder<RouteClassification> builder)
    {
        base.Configure(builder);

        builder.HasIndex(e => e.Code)
            .IsUnique();

        builder.Property(s => s.Code)
            .HasMaxLength(32);
    }
}
