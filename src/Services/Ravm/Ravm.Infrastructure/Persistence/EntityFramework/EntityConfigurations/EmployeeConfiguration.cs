namespace Ravm.Infrastructure.Persistence.EntityFramework.EntityConfigurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ravm.Domain.Models;
using Ravm.Infrastructure.Persistence.EntityFramework.Common;

public class EmployeeConfiguration : PersonBaseConfiguration<Employee>
{
    public override void Configure(EntityTypeBuilder<Employee> builder)
    {
        base.Configure(builder);

        builder.HasIndex(s => s.StaffNumber)
            .IsUnique();

        builder.Property(s => s.StaffNumber)
            .HasMaxLength(32);
    }
}
