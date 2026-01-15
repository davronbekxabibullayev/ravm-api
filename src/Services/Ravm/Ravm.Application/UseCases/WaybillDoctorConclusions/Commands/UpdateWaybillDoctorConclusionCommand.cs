namespace Ravm.Application.UseCases.WaybillDoctorConclusions.Commands;

using Microsoft.EntityFrameworkCore;

public record UpdateWaybillDoctorConclusionCommand(
Guid Id,
Guid WaybillDetailId,
Guid WaybillDriverId,
string? Pressure,
string? Pulse,
string? Temperature,
string? Note,
bool Permitted) : IRequest;

internal class UpdateWaybillDoctorConclusionCommandHandler(
    IAppDbContext dbContext,
    IMapper mapper,
    ICurrentUser currentUser) : IRequestHandler<UpdateWaybillDoctorConclusionCommand>
{
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task Handle(UpdateWaybillDoctorConclusionCommand request, CancellationToken cancellationToken)
    {
        var waybillDoctorConclusion = await GetWaybillDoctorConclusionAsync(request.Id)
            ?? throw new NotFoundException(nameof(WaybillDoctorConclusion), request.Id);

        var detail = await GetDetail(dbContext, request);

        mapper.Map(request, waybillDoctorConclusion);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static async Task<WaybillDetail> GetDetail(IAppDbContext dbContext, UpdateWaybillDoctorConclusionCommand request)
    {
        return await dbContext.WaybillDetails
                  .AsTracking()
                  .FirstOrDefaultAsync(a => a.Id == request.WaybillDetailId)
                  ?? throw new NotFoundException(nameof(WaybillDetail), request.WaybillDetailId);
    }

    private Task<WaybillDoctorConclusion?> GetWaybillDoctorConclusionAsync(Guid id)
    {
        return dbContext.WaybillDoctorConclusions
            .AsTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }
}
