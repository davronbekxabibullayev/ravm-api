namespace Ravm.Infrastructure.Persistence.EntityFramework.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ravm.Domain.Models;

public class WaybillMechanicConclusionConfiguration : IEntityTypeConfiguration<WaybillMechanicConclusion>
{
    public void Configure(EntityTypeBuilder<WaybillMechanicConclusion> builder)
    {
        builder.Property(x => x.MechanicId).IsRequired();
        builder.Property(x => x.WaybillDetailId).IsRequired();
    }
}
