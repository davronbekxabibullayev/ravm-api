namespace Ravm.Api.Models.VehicleModels;

public class UpdateVehicleModelRequest
{
    public required string Name { get; set; }
    public required string NameRu { get; set; }
    public string? NameUz { get; set; }
    public string? NameKa { get; set; }
    public required string Code { get; set; }
    public required Guid VehicleMarkId { get; set; }
    public double? FuelRate { get; set; }
    public double? FuelRateWithTrailer { get; set; }
    public double? FuelRateLoaded { get; set; }
    public double? FuelRateEngineOperation { get; set; }
    public double? FuelRateLoadedEngineOperation { get; set; }
}
