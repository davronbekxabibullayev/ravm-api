namespace Ravm.Infrastructure.Persistence.EntityFramework.EntityConfigurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ravm.Domain.Models;
using Ravm.Infrastructure.Persistence.EntityFramework.Common;

public class LocalityConfiguration : LocalizableEntityConfiguration<Locality>
{
    public override void Configure(EntityTypeBuilder<Locality> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => x.Code).IsUnique();
        builder.Property(x => x.Code).HasMaxLength(32);
        builder.Property(x => x.StateCode).HasMaxLength(32);
    }
}
