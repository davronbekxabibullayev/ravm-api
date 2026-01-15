namespace Ravm.Application.UseCases.WaybillTasks.Commands;

using Microsoft.EntityFrameworkCore;

public record UpdateWaybillTaskCommand(
Guid Id,
string Number,
string? Customer,
string? CargoInfo,
string? Note,
int TripsAmount,
DateTimeOffset Date,
TimeSpan StartTime,
TimeSpan EndTime,
double Distance,
string? AddressTo,
string? AddressFrom,
Guid WaybillId) : IRequest;

internal class UpdateWaybillTaskCommandHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<UpdateWaybillTaskCommand>
{
    public async Task Handle(UpdateWaybillTaskCommand request, CancellationToken cancellationToken)
    {
        var waybillTask = await GetWaybillTaskAsync(request.Id)
            ?? throw new NotFoundException(nameof(WaybillTask), request.Id);

        mapper.Map(request, waybillTask);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private Task<WaybillTask?> GetWaybillTaskAsync(Guid id)
    {
        return dbContext.WaybillTasks
            .AsTracking()
            .FirstOrDefaultAsync(t => t.Id == id);
    }
}
