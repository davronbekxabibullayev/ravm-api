namespace Ravm.Api.Models.WaybillDetails;

public class UpdateWaybillDetailRequest
{
    public Guid? ReceivedDriverId { get; set; }
    public Guid? ReturnedDriverId { get; set; }
    public Guid? WaybillTaskId { get; set; }
    public Guid? IdleReasonId { get; set; }
    public Guid? DispatcherId { get; set; }
    public Guid? ManagerId { get; set; }
    public DateTimeOffset Date { get; set; }
    public DateTimeOffset PlannedStartTime { get; set; }
    public DateTimeOffset PlannedEndTime { get; set; }
    public bool IsDefault { get; set; }
    public Guid? PermittedMechanicId { get; set; }
    public Guid? ReceivedMechanicId { get; set; }
    public bool IsVehicleOk { get; set; }
    public double SpeedometerIndication { get; set; }
    public double ReturnSpeedometer { get; set; }
    public Guid WaybillId { get; set; }
    public TimeSpan? КeserveDutyTime { get; set; }
    public TimeSpan? UnjustifiedTime { get; set; }
    public TimeSpan? IdleTime { get; set; }
    public TimeSpan? NightOrHolidayTime { get; set; }
    public int? ScheduledRoutesCount { get; set; }
    public int? ActuallyRoutesCount { get; set; }
}
