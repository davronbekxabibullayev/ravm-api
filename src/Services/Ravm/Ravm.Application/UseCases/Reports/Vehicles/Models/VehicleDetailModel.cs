namespace Ravm.Application.UseCases.Reports.Vehicles.Models;

public class VehicleDetailModel
{
    public DateTime Date { get; set; }
    public required string WaybillNumber { get; set; }
    public string? RouteName { get; set; }
    public double Distance { get; set; }
    public double Fuel { get; set; }
    public string? TaskNumber { get; set; }
}
