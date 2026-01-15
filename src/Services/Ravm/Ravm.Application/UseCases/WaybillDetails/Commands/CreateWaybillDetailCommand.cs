namespace Ravm.Application.UseCases.WaybillDetails.Commands;

public record CreateWaybillDetailCommand : IRequest
{
    public Guid? WaybillTaskId { get; set; }
    public Guid? DispatcherId { get; set; }
    public Guid? ManagerId { get; set; }
    public Guid? IdleReasonId { get; set; }
    public DateTimeOffset Date { get; set; }
    public DateTimeOffset PlannedStartTime { get; set; }
    public DateTimeOffset PlannedEndTime { get; set; }
    public bool IsDefault { get; set; }
    public Guid WaybillId { get; set; }
    public TimeSpan? КeserveDutyTime { get; set; }
    public TimeSpan? UnjustifiedTime { get; set; }
    public TimeSpan? IdleTime { get; set; }
    public TimeSpan? NightOrHolidayTime { get; set; }
    public int? ScheduledRoutesCount { get; set; }
    public int? ActuallyRoutesCount { get; set; }
}

internal sealed class CreateWaybillDetailCommandHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<CreateWaybillDetailCommand>
{
    public async Task Handle(CreateWaybillDetailCommand command, CancellationToken cancellationToken)
    {
        var plannedStartTime = new DateTime(
            command.PlannedStartTime.Year,
            command.PlannedStartTime.Month,
            command.PlannedStartTime.Day,
            8, 0, 0, DateTimeKind.Utc);

        var plannedEndTime = new DateTime(
            command.PlannedEndTime.Year,
            command.PlannedEndTime.Month,
            command.PlannedEndTime.Day,
            20, 0, 0, DateTimeKind.Utc);

        var waybillDetail = mapper.Map<WaybillDetail>(command);

        waybillDetail.PlannedStartTime = plannedStartTime;
        waybillDetail.PlannedEndTime = plannedEndTime;

        await dbContext.WaybillDetails.AddAsync(waybillDetail, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
