namespace Ravm.Application.UseCases.VehicleMarks.Commands;

using Microsoft.EntityFrameworkCore;

public record DeleteVehicleMarkCommand(Guid Id) : IRequest;

internal class DeleteVehicleMarkCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteVehicleMarkCommand>
{
    public async Task Handle(DeleteVehicleMarkCommand request, CancellationToken cancellationToken)
    {
        var vehicleMark = await dbContext.VehicleMarks
            .Where(w => w.Id == request.Id)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.IsDeleted, true), cancellationToken);

        if (vehicleMark == 0)
        {
            throw new NotFoundException(nameof(VehicleMark), request.Id);
        }
    }
}
