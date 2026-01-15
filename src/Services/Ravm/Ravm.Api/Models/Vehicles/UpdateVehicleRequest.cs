namespace Ravm.Api.Models.Vehicles;

public class UpdateVehicleRequest
{
    public Guid OrganizationId { get; set; }
    public Guid VehicleModelId { get; set; }
    public required string StateNumber { get; set; }
    public required string GarageNumber { get; set; }
    public string? Vin { get; set; }
    public string? ChassisNumber { get; set; }
}
