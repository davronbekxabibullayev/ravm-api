namespace Ravm.Application.UseCases.Vehicles.Models;

public class VehicleItem
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public Guid VehicleModelId { get; set; }
    public required string StateNumber { get; set; }
    public required string GarageNumber { get; set; }
    public string? Vin { get; set; }
    public string? ChassisNumber { get; set; }
}
