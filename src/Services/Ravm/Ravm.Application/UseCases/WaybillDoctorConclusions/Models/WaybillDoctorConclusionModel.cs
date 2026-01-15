namespace Ravm.Application.UseCases.WaybillDoctorConclusions.Models;

using Ravm.Application.UseCases.WaybillDrivers.Models;

public class WaybillDoctorConclusionModel
{
    public Guid Id { get; set; }
    public Guid WaybillDetailId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid WaybillDriverId { get; set; }
    public WaybillDriverModel? WaybillDriver { get; set; }
    public Guid WaybillId { get; set; }
    public string? Pressure { get; set; }
    public string? Pulse { get; set; }
    public string? Temperature { get; set; }
    public string? Note { get; set; }
    public bool Permitted { get; set; }
}
