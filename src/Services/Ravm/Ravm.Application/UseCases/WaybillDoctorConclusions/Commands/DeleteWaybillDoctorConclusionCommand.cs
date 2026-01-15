namespace Ravm.Application.UseCases.WaybillDoctorConclusions.Commands;

using Microsoft.EntityFrameworkCore;

public record DeleteWaybillDoctorConclusionCommand(Guid Id) : IRequest;

internal class DeleteWaybillDoctorConclusionCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteWaybillDoctorConclusionCommand>
{

    public async Task Handle(DeleteWaybillDoctorConclusionCommand request, CancellationToken cancellationToken)
    {
        var waybillDoctorConclusion = await dbContext.WaybillDoctorConclusions
            .Where(x => x.Id.Equals(request.Id))
            .ExecuteUpdateAsync(a => a.SetProperty(b => b.IsDeleted, true), cancellationToken);

        if (waybillDoctorConclusion == 0)
        {
            throw new NotFoundException(nameof(waybillDoctorConclusion), request.Id);
        }
    }
}
