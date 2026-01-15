namespace Ravm.Infrastructure.Persistence.EntityFramework.EntityConfigurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ravm.Domain.Models;
using Ravm.Infrastructure.Persistence.EntityFramework.Common;

public class VehicleMarkConfiguration : LocalizableEntityConfiguration<VehicleMark>
{
    public override void Configure(EntityTypeBuilder<VehicleMark> builder)
    {
        base.Configure(builder);

        builder.HasIndex(s => s.Code)
            .IsUnique();

        builder.Property(s => s.Code)
            .HasMaxLength(32);
    }
}
