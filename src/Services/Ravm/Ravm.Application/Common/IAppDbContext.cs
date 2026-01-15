namespace Ravm.Application.Common;

using Microsoft.EntityFrameworkCore;

public interface IAppDbContext : IDbContext
{
    DbSet<City> Cities { get; set; }
    DbSet<Country> Countries { get; set; }
    DbSet<Employee> Employees { get; set; }
    DbSet<EmployeeOccupation> EmployeeOccupations { get; set; }
    DbSet<EmployeeSpecialization> EmployeeSpecializations { get; set; }
    DbSet<Locality> Localities { get; set; }
    DbSet<Occupation> Occupations { get; set; }
    DbSet<Organization> Organizations { get; set; }
    DbSet<OrganizationAddress> OrganizationAddresses { get; set; }
    DbSet<OrganizationContact> OrganizationContacts { get; set; }
    DbSet<Region> Regions { get; set; }
    DbSet<Route> Routes { get; set; }
    DbSet<RouteClassification> RouteClassifications { get; set; }
    DbSet<RouteStopPoint> RouteStopPoints { get; set; }
    DbSet<StopPoint> StopPoints { get; set; }
    DbSet<RouteVehicleModel> RouteVehicleModels { get; set; }
    DbSet<Specialization> Specializations { get; set; }
    DbSet<Vehicle> Vehicles { get; set; }
    DbSet<VehicleModel> VehicleModels { get; set; }
    DbSet<VehicleMark> VehicleMarks { get; set; }
    DbSet<Waybill> Waybills { get; set; }
    DbSet<WaybillDetail> WaybillDetails { get; set; }
    DbSet<WaybillDriver> WaybillDrivers { get; set; }
    DbSet<WaybillFuel> WaybillFuels { get; set; }
    DbSet<WaybillTask> WaybillTasks { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<WaybillDoctorConclusion> WaybillDoctorConclusions { get; set; }
    DbSet<WaybillMechanicConclusion> WaybillMechanicConclusions { get; set; }
    DbSet<Reason> Reasons { get; set; }
}
