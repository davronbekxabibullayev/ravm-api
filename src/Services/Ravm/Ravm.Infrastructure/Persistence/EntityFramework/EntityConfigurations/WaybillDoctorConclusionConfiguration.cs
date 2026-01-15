namespace Ravm.Infrastructure.Persistence.EntityFramework.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ravm.Domain.Models;

public class WaybillDoctorConclusionConfiguration : IEntityTypeConfiguration<WaybillDoctorConclusion>
{
    public void Configure(EntityTypeBuilder<WaybillDoctorConclusion> builder)
    {
        builder.Property(x => x.WaybillDriverId).IsRequired();
        builder.Property(x => x.DoctorId).IsRequired();
    }
}
