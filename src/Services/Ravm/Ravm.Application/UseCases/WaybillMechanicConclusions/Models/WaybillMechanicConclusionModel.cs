namespace Ravm.Application.UseCases.WaybillMechanicConclusions.Models;

using Ravm.Application.UseCases.Employees.Models;
using Ravm.Application.UseCases.Vehicles.Models;
using Ravm.Domain.Enums;

public class WaybillMechanicConclusionModel
{
    public Guid Id { get; set; }
    public Guid MechanicId { get; set; }
    public Guid WaybillDetailId { get; set; }
    public Guid WaybillId { get; set; }
    public Guid? ReceivedDriverId { get; set; }
    public EmployeeModel? ReceivedDriver { get; set; }
    public Guid? ReturnedDriverId { get; set; }
    public EmployeeModel? ReturnedDriver { get; set; }
    public VehicleItem? Vehicle { get; set; }
    public bool IsEngineHealthy { get; set; }
    public bool IsTireHealthy { get; set; }
    public bool IsBrakeHealthy { get; set; }
    public bool IsTransmissionHealthy { get; set; }
    public MechanicConclusionType MechanicConclusionType { get; set; }
    public string? Note { get; set; }
    public double SpeedometerIndication { get; set; }
    public double ReturnSpeedometer { get; set; }
    public bool IsVehicleHealthy { get; set; }
    public double FuelAmount { get; set; }
}
