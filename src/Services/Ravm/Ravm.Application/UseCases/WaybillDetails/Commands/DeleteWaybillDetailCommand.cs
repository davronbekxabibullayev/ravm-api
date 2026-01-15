namespace Ravm.Application.UseCases.WaybillDetails.Commands;

using Microsoft.EntityFrameworkCore;

public record DeleteWaybillDetailCommand(Guid Id) : IRequest;

internal class DeleteWaybillDetailCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteWaybillDetailCommand>
{
    public async Task Handle(DeleteWaybillDetailCommand command, CancellationToken cancellationToken)
    {
        var waybillDetail = await dbContext.WaybillDetails
            .Where(w => w.Id == command.Id)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.IsDeleted, true), cancellationToken);

        if (waybillDetail == 0)
        {
            throw new NotFoundException(nameof(WaybillDetail), command.Id);
        }
    }
}
