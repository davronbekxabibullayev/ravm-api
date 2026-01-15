namespace Ravm.Application.UseCases.WaybillDetails.Commands;

using Microsoft.EntityFrameworkCore;
using Ravm.Domain.Enums;

public record UpdateWaybillDetailStatusCommand(Guid Id, WaybillDetailStatus Status) : IRequest;

internal sealed class UpdateWaybillDetailStatusCommandHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateWaybillDetailStatusCommand>
{
    public async Task Handle(UpdateWaybillDetailStatusCommand command, CancellationToken cancellationToken)
    {
        var waybillDetail = await GetWaybillDetailAsync(command.Id)
            ?? throw new NotFoundException(nameof(WaybillDetail), command.Id);

        if (command.Status == WaybillDetailStatus.OnWay && IsMayPutOnWay(waybillDetail))
        {
            waybillDetail.Status = command.Status;
            waybillDetail.ActualStartTime = DateTimeOffset.UtcNow.AddHours(5);
        }

        if (command.Status == WaybillDetailStatus.Completed && IsCompleted(waybillDetail))
        {
            if (command.Status == WaybillDetailStatus.Completed)
            {
                waybillDetail.ActualEndTime = DateTimeOffset.UtcNow.AddHours(5);
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static bool IsCompleted(WaybillDetail waybillDetail)
    {
        var mayCompeleted = waybillDetail.MechanicConclusions.Any(a => a.MechanicConclusionType.Equals(MechanicConclusionType.acceptance));

        if (!mayCompeleted)
            throw new AppException("Mechanic conclusion was not found with type Acceptance in this WaybillDetail");

        mayCompeleted = waybillDetail.ActualEndTime == null;

        if (!mayCompeleted)
            throw new AppException("WaybillDetail already is completed");

        return mayCompeleted;
    }

    private static bool IsMayPutOnWay(WaybillDetail waybillDetail)
    {
        var isMayPut = waybillDetail.MechanicConclusions.Any(a => a.MechanicConclusionType == MechanicConclusionType.put
               && a.IsVehicleHealthy);

        if (!isMayPut)
            throw new NotFoundException($"WaybillMechanicConclusion not found with type {MechanicConclusionType.put} and Vehicle healthy is true");

        isMayPut = !waybillDetail.MechanicConclusions.Any(a => a.MechanicConclusionType == MechanicConclusionType.acceptance);

        if (!isMayPut)
            throw new AppException("WaybillMechanicConclusion already was completed");

        isMayPut = waybillDetail.WaybillDoctorConclusions.Any(doc => doc.Permitted == false);
        if (isMayPut)
            throw new AppException("DoctorConclusion is not permitted");


        return true;
    }

    private Task<WaybillDetail?> GetWaybillDetailAsync(Guid id)
    {
        return dbContext.WaybillDetails
            .Include(a => a.MechanicConclusions)
            .Include(a => a.WaybillDoctorConclusions)
            .AsTracking()
            .FirstOrDefaultAsync(t => t.Id == id);
    }
}
