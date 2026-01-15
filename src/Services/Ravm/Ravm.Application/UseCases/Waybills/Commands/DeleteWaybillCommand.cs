namespace Ravm.Application.UseCases.Waybills.Commands;

using Microsoft.EntityFrameworkCore;

public record DeleteWaybillCommand(Guid Id) : IRequest;

internal class DeleteWaybillCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteWaybillCommand>
{
    public async Task Handle(DeleteWaybillCommand request, CancellationToken cancellationToken)
    {
        var waybill = await dbContext.Waybills
            .Where(x => x.Id.Equals(request.Id))
            .ExecuteUpdateAsync(a => a.SetProperty(b => b.IsDeleted, true), cancellationToken);

        if (waybill == 0)
        {
            throw new NotFoundException(nameof(Waybill), request.Id);
        }
    }
}
