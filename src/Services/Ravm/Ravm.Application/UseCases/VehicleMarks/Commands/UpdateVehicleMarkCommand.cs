namespace Ravm.Application.UseCases.VehicleMarks.Commands;

using Microsoft.EntityFrameworkCore;

public record UpdateVehicleMarkCommand(
    Guid Id,
    string Name,
    string NameRu,
    string? NameUz,
    string? NameKa,
    string Code) : IRequest;

internal class UpdateVehicleMarkCommandHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<UpdateVehicleMarkCommand>
{
    public async Task Handle(UpdateVehicleMarkCommand request, CancellationToken cancellationToken)
    {
        var vehicleMark = await GetVehicleMarkAsync(request.Id)
            ?? throw new NotFoundException(nameof(VehicleMark), request.Id);

        mapper.Map(request, vehicleMark);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private Task<VehicleMark?> GetVehicleMarkAsync(Guid id)
    {
        return dbContext.VehicleMarks
                    .AsTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
    }
}
