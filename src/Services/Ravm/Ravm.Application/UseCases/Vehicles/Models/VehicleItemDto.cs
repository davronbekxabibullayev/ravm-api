namespace Ravm.Application.UseCases.Vehicles.Models;

using Ravm.Application.UseCases.Organizations.Models;
using Ravm.Application.UseCases.VehicleModels.Models;

public class VehicleItemDto
{
    public Guid Id { get; set; }
    public OrganizationModel? Organization { get; set; }
    public VehicleModelModel? VehicleModel { get; set; }
    public required string StateNumber { get; set; }
    public required string GarageNumber { get; set; }
    public string? Vin { get; set; }
    public string? ChassisNumber { get; set; }
}
