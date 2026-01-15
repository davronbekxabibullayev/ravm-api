namespace Ravm.Application.UseCases.VehicleModels.Models;

using Ravm.Application.Common.Models;

public class VehicleModelModel : LocalizableName
{
    public Guid Id { get; set; }
    public Guid VehicleMarkId { get; set; }
    public required string Code { get; set; }
    public double? FuelRate { get; set; }
    public double? FuelRateWithTrailer { get; set; }
    public double? FuelRateLoaded { get; set; }
    public double? FuelRateEngineOperation { get; set; }
    public double? FuelRateLoadedEngineOperation { get; set; }
}
