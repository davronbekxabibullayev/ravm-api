namespace Ravm.Infrastructure.Persistence.EntityFramework;

using Microsoft.EntityFrameworkCore;
using Ravm.Domain.Models;
using Ravm.Application.Common;
using System.Reflection;
using Ravm.Domain.Common;
using System.Threading.Tasks;
using System.Threading;
using Ravm.Infrastructure.Persistence.EntityFramework.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using global::EntityFramework.Exceptions.PostgreSQL;

public class AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUser currentUser)
        : IdentityDbContext<User, Role, Guid>(options), IAppDbContext
{
    private readonly ICurrentUser _currentUser = currentUser;

    public DbSet<City> Cities { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeOccupation> EmployeeOccupations { get; set; }
    public DbSet<EmployeeSpecialization> EmployeeSpecializations { get; set; }
    public DbSet<Locality> Localities { get; set; }
    public DbSet<Occupation> Occupations { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<OrganizationAddress> OrganizationAddresses { get; set; }
    public DbSet<OrganizationContact> OrganizationContacts { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<RouteClassification> RouteClassifications { get; set; }
    public DbSet<RouteStopPoint> RouteStopPoints { get; set; }
    public DbSet<StopPoint> StopPoints { get; set; }
    public DbSet<RouteVehicleModel> RouteVehicleModels { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<VehicleModel> VehicleModels { get; set; }
    public DbSet<VehicleMark> VehicleMarks { get; set; }
    public DbSet<Waybill> Waybills { get; set; }
    public DbSet<WaybillDetail> WaybillDetails { get; set; }
    public DbSet<WaybillDriver> WaybillDrivers { get; set; }
    public DbSet<WaybillFuel> WaybillFuels { get; set; }
    public DbSet<WaybillTask> WaybillTasks { get; set; }
    public DbSet<WaybillDoctorConclusion> WaybillDoctorConclusions { get; set; }
    public DbSet<WaybillMechanicConclusion> WaybillMechanicConclusions { get; set; }
    public DbSet<Reason> Reasons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        optionsBuilder.UseExceptionProcessor();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.ConfigureLocalizableEntities();
        modelBuilder.SetSoftDeleteFilter();
    }

    public override int SaveChanges()
    {
        SetAuditableEntity();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetAuditableEntity();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void SetAuditableEntity()
    {
        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                if (entry.Entity.CreatedById.Equals(Guid.Empty))
                {
                    entry.Entity.CreatedById = _currentUser.UserId;
                    entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                    entry.Entity.UpdatedById = _currentUser.UserId;
                    entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
                }
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedById = _currentUser.UserId;
                entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
            }
        }
    }
}
