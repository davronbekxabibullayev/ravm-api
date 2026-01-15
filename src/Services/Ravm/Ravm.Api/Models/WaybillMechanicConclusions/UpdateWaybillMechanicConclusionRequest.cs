namespace Ravm.Api.Models.WaybillMechanicConclusions;

using Ravm.Domain.Enums;

public class UpdateWaybillMechanicConclusionRequest
{
    public Guid WaybillDetailId { get; set; }
    public Guid? ReceivedDriverId { get; set; }
    public Guid? ReturnedDriverId { get; set; }
    public bool IsEngineHealthy { get; set; }
    public bool IsTireHealthy { get; set; }
    public bool IsBrakeHealthy { get; set; }
    public bool IsTransmissionHealthy { get; set; }
    public MechanicConclusionType MechanicConclusionType { get; set; }
    public string? Note { get; set; }
    public bool IsVehicleHealthy { get; set; }
    public double SpeedometerIndication { get; set; }
    public double ReturnSpeedometer { get; set; }
    public double FuelAmount { get; set; }
}
