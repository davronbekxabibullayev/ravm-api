namespace Ravm.Application.UseCases.WaybillDrivers.Commands;

using Microsoft.EntityFrameworkCore;

public record DeleteWaybillDriverCommand(Guid Id) : IRequest;

internal class DeleteWaybillDriverCommandHandler(IAppDbContext dbContext)
    : IRequestHandler<DeleteWaybillDriverCommand>
{
    public async Task Handle(DeleteWaybillDriverCommand request, CancellationToken cancellationToken)
    {
        var waybillDriver = await dbContext.WaybillDrivers
            .FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken)
            ?? throw new NotFoundException(nameof(WaybillDriver), request.Id);

        dbContext.WaybillDrivers.Remove(waybillDriver);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
