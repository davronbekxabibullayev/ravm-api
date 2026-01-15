namespace Ravm.Application.UseCases.WaybillFuels.Commands;

using Microsoft.EntityFrameworkCore;

public record DeleteWaybillFuelCommand(Guid Id) : IRequest;

internal class DeleteWaybillFuelCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteWaybillFuelCommand>
{
    public async Task Handle(DeleteWaybillFuelCommand request, CancellationToken cancellationToken)
    {
        var deleteRows = await dbContext.WaybillFuels
            .Where(x => x.Id.Equals(request.Id))
            .ExecuteUpdateAsync(a => a.SetProperty(b => b.IsDeleted, true), cancellationToken);

        if (deleteRows == 0)
        {
            throw new NotFoundException(nameof(WaybillFuel), request.Id);
        }
    }
}
