namespace Ravm.Application.UseCases.WaybillMechanicConclusions.Commands;

using Microsoft.EntityFrameworkCore;
using Ravm.Domain.Enums;

public record CreateWaybillMechanicConclusionCommand : IRequest
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

internal class CreateWaybillMechanicConclusionCommandHandler(IAppDbContext dbContext, ICurrentUser currentUser)
    : IRequestHandler<CreateWaybillMechanicConclusionCommand>
{
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task Handle(CreateWaybillMechanicConclusionCommand request, CancellationToken cancellationToken)
    {
        await UpdateDetail(request, _currentUser.EmployeeId);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task UpdateDetail(CreateWaybillMechanicConclusionCommand request, Guid currentEmployeeId)
    {
        var detail = await dbContext.WaybillDetails
            .AsTracking()
            .FirstOrDefaultAsync(a => a.Id == request.WaybillDetailId)
            ?? throw new NotFoundException(nameof(WaybillDetail), request.WaybillDetailId);

        if (request.MechanicConclusionType == MechanicConclusionType.put)
        {
            detail.ReceivedDriverId = request.ReceivedDriverId;
            detail.SpeedometerIndication = request.SpeedometerIndication;
            detail.IsVehicleOk = request.IsVehicleHealthy;
            detail.PermittedMechanicId = currentEmployeeId;
        }

        else
        {
            detail.ReturnedDriverId = request.ReturnedDriverId;
            detail.ReturnSpeedometer = request.ReturnSpeedometer;
            detail.IsReturnVehicleOk = request.IsVehicleHealthy;
            detail.ReceivedMechanicId = currentEmployeeId;
        }

        detail.MechanicConclusions.Add(NewWaybillMechanicConclusion(request, currentEmployeeId));
    }

    private static WaybillMechanicConclusion NewWaybillMechanicConclusion(CreateWaybillMechanicConclusionCommand request, Guid currentEmployeeId)
    {
        return new WaybillMechanicConclusion
        {
            MechanicId = currentEmployeeId,
            ReceivedDriverId = request.ReceivedDriverId,
            ReturnedDriverId = request.ReturnedDriverId,
            IsEngineHealthy = request.IsEngineHealthy,
            IsTireHealthy = request.IsTireHealthy,
            IsBrakeHealthy = request.IsBrakeHealthy,
            IsTransmissionHealthy = request.IsTransmissionHealthy,
            MechanicConclusionType = request.MechanicConclusionType,
            Note = request.Note,
            IsVehicleHealthy = request.IsVehicleHealthy,
            SpeedometerIndication = request.SpeedometerIndication,
            ReturnSpeedometer = request.ReturnSpeedometer,
            FuelAmount = request.FuelAmount
        };
    }
}
