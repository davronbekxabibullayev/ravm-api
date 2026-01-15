namespace Ravm.Infrastructure.Persistence.EntityFramework.EntityConfigurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ravm.Domain.Models;
using Ravm.Infrastructure.Persistence.EntityFramework.Common;

public class OrganizationConfiguration : LocalizableAuditableEntityConfiguration<Organization>
{
    public override void Configure(EntityTypeBuilder<Organization> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => x.Code)
             .IsUnique();
        builder.Property(a => a.Code)
            .HasMaxLength(32);
        builder.Property(a => a.Tin)
            .HasMaxLength(32);
        builder.Property(a => a.Oked)
            .HasMaxLength(32);
        builder.Property(a => a.Okonx)
            .HasMaxLength(32);
    }
}
