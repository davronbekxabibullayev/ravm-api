namespace Uds.Common.Infrastructure.EntityFramework.EntityConfigurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ravm.Domain.Models;
using Ravm.Infrastructure.Persistence.EntityFramework.Common;

public class CountryConfiguration : LocalizableEntityConfiguration<Country>
{
    public override void Configure(EntityTypeBuilder<Country> builder)
    {
        base.Configure(builder);

        builder.HasIndex(s => s.Code)
            .IsUnique();

        builder.Property(s => s.Code)
            .HasMaxLength(32);

        builder.Property(s => s.StateCode)
            .HasMaxLength(32);
    }
}

