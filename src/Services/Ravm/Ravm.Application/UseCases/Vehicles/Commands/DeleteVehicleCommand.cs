namespace Ravm.Application.UseCases.Vehicles.Commands;

using Microsoft.EntityFrameworkCore;

public record DeleteVehicleCommand(Guid Id) : IRequest;

internal class DeleteVehicleCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteVehicleCommand>
{
    public async Task Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
    {
        var vehicle = await dbContext.Vehicles
            .Where(w => w.Id == request.Id)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.IsDeleted, true), cancellationToken);

        if (vehicle == 0)
        {
            throw new NotFoundException(nameof(Vehicle), request.Id);
        }
    }
}
