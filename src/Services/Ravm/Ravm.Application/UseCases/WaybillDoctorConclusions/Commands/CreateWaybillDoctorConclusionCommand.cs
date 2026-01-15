namespace Ravm.Application.UseCases.WaybillDoctorConclusions.Commands;

using Microsoft.EntityFrameworkCore;

public record CreateWaybillDoctorConclusionCommand : IRequest
{
    public Guid WaybillDetailId { get; set; }
    public Guid WaybillDriverId { get; set; }
    public string? Pressure { get; set; }
    public string? Pulse { get; set; }
    public string? Temperature { get; set; }
    public string? Note { get; set; }
    public bool Permitted { get; set; }
}

internal class CreateWaybillDoctorConclusionCommandHandler(
    IAppDbContext dbContext,
    ICurrentUser currentUser) : IRequestHandler<CreateWaybillDoctorConclusionCommand>
{
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task Handle(CreateWaybillDoctorConclusionCommand request, CancellationToken cancellationToken)
    {
        var waybillDoctorConclusion = NewWaybillDoctorConclusion(request, _currentUser.EmployeeId);

        var detail = await dbContext.WaybillDetails
                  .AsTracking()
                  .FirstOrDefaultAsync(a => a.Id == request.WaybillDetailId, cancellationToken)
                  ?? throw new NotFoundException(nameof(WaybillDetail), request.WaybillDetailId);

        await dbContext.WaybillDoctorConclusions.AddAsync(waybillDoctorConclusion, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private WaybillDoctorConclusion NewWaybillDoctorConclusion(CreateWaybillDoctorConclusionCommand request, Guid currentEmployeeId)
    {
        return new WaybillDoctorConclusion
        {
            WaybillDetailId = request.WaybillDetailId,
            DoctorId = currentEmployeeId,
            WaybillDriverId = request.WaybillDriverId,
            Pressure = request.Pressure,
            Pulse = request.Pulse,
            Temperature = request.Temperature,
            Note = request.Note,
            Permitted = request.Permitted
        };
    }
}
