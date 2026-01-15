namespace Ravm.Application.UseCases.WaybillTasks.Commands;

using Microsoft.EntityFrameworkCore;

public record DeleteWaybillTaskCommand(Guid Id) : IRequest;

internal class DeleteWaybillTaskCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteWaybillTaskCommand>
{
    public async Task Handle(DeleteWaybillTaskCommand request, CancellationToken cancellationToken)
    {
        var deleteRows = await dbContext.WaybillTasks
            .Where(x => x.Id.Equals(request.Id))
            .ExecuteUpdateAsync(a => a.SetProperty(b => b.IsDeleted, true), cancellationToken);

        if(deleteRows == 0)
        {
            throw new NotFoundException(nameof(WaybillTask), request.Id);
        }
    }
}
