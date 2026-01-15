namespace Ravm.Infrastructure.Persistence.EntityFramework.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ravm.Domain.Models;

public class StopPointConfiguration : IEntityTypeConfiguration<StopPoint>
{
    public void Configure(EntityTypeBuilder<StopPoint> builder)
    {
        builder.HasIndex(e => e.Code)
           .IsUnique();

        builder.Property(e => e.Code)
            .HasMaxLength(32);
    }
}
