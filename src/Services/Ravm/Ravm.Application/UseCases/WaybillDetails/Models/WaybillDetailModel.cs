namespace Ravm.Application.UseCases.WaybillDetails.Models;

using Ravm.Application.UseCases.Vehicles.Models;
using Ravm.Application.UseCases.WaybillDoctorConclusions.Models;
using Ravm.Application.UseCases.WaybillFuels.Models;
using Ravm.Application.UseCases.WaybillMechanicConclusions.Models;
using Ravm.Application.UseCases.Waybills.Models;
using Ravm.Domain.Enums;

public class WaybillDetailModel
{
    public Guid Id { get; set; }
    public Guid? ReceivedDriverId { get; set; }
    public Guid? ReturnedDriverId { get; set; }
    public Guid? WaybillTaskId { get; set; }
    public Guid? IdleReasonId { get; set; }
    public Guid? DispatcherId { get; set; }
    public Guid? ManagerId { get; set; }
    public DateTimeOffset Date { get; set; }
    public DateTimeOffset PlannedStartTime { get; set; }
    public DateTimeOffset PlannedEndTime { get; set; }
    public DateTimeOffset? ActualStartTime { get; set; }
    public DateTimeOffset? ActualEndTime { get; set; }
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
    public WaybillDetailStatus Status { get; set; }

    public WaybillModel? Waybill { get; set; }
    public VehicleItemDto? Vehicle { get; set; }
    public ICollection<WaybillMechanicConclusionModel>? MechanicConclusions { get; set; }
    public ICollection<WaybillDoctorConclusionModel>? WaybillDoctorConclusions { get; set; }
    public ICollection<WaybillFuelModel>? WaybillFuels { get; set; }
}
