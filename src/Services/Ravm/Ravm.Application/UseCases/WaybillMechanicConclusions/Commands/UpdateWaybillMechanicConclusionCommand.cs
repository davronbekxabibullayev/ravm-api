namespace Ravm.Application.UseCases.WaybillMechanicConclusions.Commands;

using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ravm.Domain.Enums;
using Ravm.Domain.Models;

public record UpdateWaybillMechanicConclusionCommand(
Guid Id,
Guid WaybillDetailId,
Guid? ReceivedDriverId,
Guid? ReturnedDriverId,
bool IsEngineHealthy,
bool IsTireHealthy,
bool IsBrakeHealthy,
bool IsTransmissionHealthy,
MechanicConclusionType MechanicConclusionType,
string? Note,
double SpeedometerIndication,
double ReturnSpeedometer,
bool IsVehicleHealthy,
double FuelAmount) : IRequest;

internal class UpdateWaybillMechanicConclusionCommandHandler(
    IAppDbContext dbContext,
    IMapper mapper,
    ICurrentUser currentUser) : IRequestHandler<UpdateWaybillMechanicConclusionCommand>
{
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task Handle(UpdateWaybillMechanicConclusionCommand request, CancellationToken cancellationToken)
    {
        var waybillMechanicConclusion = await GetWaybillMechanicConclusionAsync(request.Id)
            ?? throw new NotFoundException(nameof(WaybillMechanicConclusion), request.Id);

        var employee = await dbContext.Employees.FirstOrDefaultAsync(x => x.UserId == _currentUser.UserId, cancellationToken)
           ?? throw new NotFoundException(nameof(User), _currentUser.UserId);

        mapper.Map(request, waybillMechanicConclusion);

        await UpdateDetail(dbContext, request, employee.Id, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task UpdateDetail(IAppDbContext dbContext, UpdateWaybillMechanicConclusionCommand request, Guid currentEmployeeId, CancellationToken cancellationToken)
    {
        var detail = await dbContext.WaybillDetails
                    .AsTracking()
                    .FirstOrDefaultAsync(a => a.Id == request.WaybillDetailId, cancellationToken)
                    ?? throw new NotFoundException(nameof(WaybillDetail), request.WaybillDetailId);

        if (request.MechanicConclusionType == MechanicConclusionType.put)
        {
            detail.ReceivedDriverId = request.ReceivedDriverId;
            detail.SpeedometerIndication = request.SpeedometerIndication;
            detail.IsVehicleOk = request.IsVehicleHealthy;
            detail.PermittedMechanicId = currentEmployeeId;
        }

        if (request.MechanicConclusionType == MechanicConclusionType.acceptance)
        {
            detail.ReturnedDriverId = request.ReturnedDriverId;
            detail.ReturnSpeedometer = request.ReturnSpeedometer;
            detail.IsReturnVehicleOk = request.IsVehicleHealthy;
            detail.ReceivedMechanicId = currentEmployeeId;
        }
    }

    private Task<WaybillMechanicConclusion?> GetWaybillMechanicConclusionAsync(Guid id)
    {
        return dbContext.WaybillMechanicConclusions
            .AsTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
