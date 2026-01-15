namespace Ravm.Application.UseCases.Reports.Vehicles.Models;

using Ravm.Application.UseCases.Vehicles.Models;

public class ReportVehicleDataSummary
{
    public VehicleItemDto? Vehicle { get; set; }
    public double Mileage { get; set; }
    public double FuelAmount { get; set; }
}
