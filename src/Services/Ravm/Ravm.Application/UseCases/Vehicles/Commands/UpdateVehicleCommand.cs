namespace Ravm.Application.UseCases.Vehicles.Commands;

using Microsoft.EntityFrameworkCore;

public record UpdateVehicleCommand(
    Guid Id,
    Guid OrganizationId,
    Guid VehicleModelId,
    string StateNumber,
    string GarageNumber,
    string? Vin,
    string? ChassisNumber) : IRequest;

internal class UpdateVehicleCommandHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<UpdateVehicleCommand>
{
    public async Task Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
    {
        var vehicle = await GetVehicleAsync(request.Id)
            ?? throw new NotFoundException(nameof(Vehicle), request.Id);

        mapper.Map(request, vehicle);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private Task<Vehicle?> GetVehicleAsync(Guid id)
    {
        return dbContext.Vehicles
                    .AsTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
    }
}
