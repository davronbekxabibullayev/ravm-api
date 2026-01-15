namespace Ravm.Application.UseCases.WaybillMechanicConclusions.Commands;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.Common;

public record DeleteWaybillMechanicConclusionCommand(Guid Id) : IRequest;

internal class DeleteWaybillMechanicConclusionCommandHandler(IAppDbContext dbContext, ICurrentUser currentUser)
    : IRequestHandler<DeleteWaybillMechanicConclusionCommand>
{
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task Handle(DeleteWaybillMechanicConclusionCommand request, CancellationToken cancellationToken)
    {
        var waybillMechanicConclusion = await dbContext.WaybillMechanicConclusions
            .Where(x => x.Id.Equals(request.Id))
            .ExecuteUpdateAsync(a => a.SetProperty(b => b.IsDeleted, true)
                                      .SetProperty(b => b.UpdatedAt, DateTime.UtcNow)
                                      .SetProperty(b => b.UpdatedById, _currentUser.UserId), cancellationToken);

        if (waybillMechanicConclusion == 0)
        {
            throw new NotFoundException(nameof(WaybillMechanicConclusion), request.Id);
        }
    }
}
