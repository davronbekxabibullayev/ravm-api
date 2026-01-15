namespace Ravm.Application.UseCases.WaybillDetails.Commands;

using Microsoft.EntityFrameworkCore;

public record UpdateWaybillDetailCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid? WaybillTaskId { get; set; }
    public Guid? IdleReasonId { get; set; }
    public Guid? DispatcherId { get; set; }
    public Guid? ManagerId { get; set; }
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
};

internal sealed class UpdateWaybillDetailCommandHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateWaybillDetailCommand>
{
    public async Task Handle(UpdateWaybillDetailCommand command, CancellationToken cancellationToken)
    {
        var waybillDetail = await GetWaybillDetailAsync(command.Id)
            ?? throw new NotFoundException(nameof(WaybillDetail), command.Id);

        command.PlannedStartTime = new DateTime(
            command.PlannedStartTime.Year,
            command.PlannedStartTime.Month,
            command.PlannedStartTime.Day,
            8, 0, 0, DateTimeKind.Utc);

        command.PlannedEndTime = new DateTime(
            command.PlannedEndTime.Year,
            command.PlannedEndTime.Month,
            command.PlannedEndTime.Day,
            20, 0, 0, DateTimeKind.Utc);

        mapper.Map(command, waybillDetail);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private Task<WaybillDetail?> GetWaybillDetailAsync(Guid id)
    {
        return dbContext.WaybillDetails
            .AsTracking()
            .FirstOrDefaultAsync(t => t.Id == id);
    }
}
