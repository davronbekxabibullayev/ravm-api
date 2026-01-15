namespace Ravm.Infrastructure.Persistence.EntityFramework.EntityConfigurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ravm.Domain.Models;
using Ravm.Infrastructure.Persistence.EntityFramework.Common;

public class SpecializationConfiguration : LocalizableEntityConfiguration<Specialization>
{
    public override void Configure(EntityTypeBuilder<Specialization> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => x.Code).IsUnique();
        builder.Property(x => x.Code).HasMaxLength(32);
    }
}
