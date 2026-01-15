namespace Ravm.Application.UseCases.VehicleModels.Commands;

using Microsoft.EntityFrameworkCore;

public record DeleteVehicleModelCommand(Guid Id) : IRequest;

internal class DeleteVehicleModelCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteVehicleModelCommand>
{
    public async Task Handle(DeleteVehicleModelCommand request, CancellationToken cancellationToken)
    {
        var vehicleModel = await dbContext.VehicleModels
            .Where(w => w.Id == request.Id)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.IsDeleted, true), cancellationToken);

        if (vehicleModel == 0)
        {
            throw new NotFoundException(nameof(VehicleModel), request.Id);
        }
    }
}
