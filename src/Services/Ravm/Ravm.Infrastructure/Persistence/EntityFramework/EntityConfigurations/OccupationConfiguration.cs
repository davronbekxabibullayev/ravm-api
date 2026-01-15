namespace Ravm.Infrastructure.Persistence.EntityFramework.EntityConfigurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ravm.Domain.Models;
using Ravm.Infrastructure.Persistence.EntityFramework.Common;

public class OccupationConfiguration : LocalizableEntityConfiguration<Occupation>
{
    public override void Configure(EntityTypeBuilder<Occupation> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => x.Code).IsUnique();
        builder.Property(x => x.Code).HasMaxLength(32);
    }
}
