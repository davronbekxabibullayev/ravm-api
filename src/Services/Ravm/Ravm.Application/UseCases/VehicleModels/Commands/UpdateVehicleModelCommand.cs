namespace Ravm.Application.UseCases.VehicleModels.Commands;

using Microsoft.EntityFrameworkCore;

public record UpdateVehicleModelCommand(
    Guid Id,
    string Name,
    string NameRu,
    string? NameUz,
    string? NameKa,
    string Code,
    Guid VehicleMarkId,
    double? FuelRate,
    double? FuelRateWithTrailer,
    double? FuelRateLoaded,
    double? FuelRateEngineOperation,
    double? FuelRateLoadedEngineOperation) : IRequest;

internal class UpdateVehicleModelCommandHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<UpdateVehicleModelCommand>
{
    public async Task Handle(UpdateVehicleModelCommand request, CancellationToken cancellationToken)
    {
        var vehicleModel = await GetVehicleModelAsync(request.Id)
            ?? throw new NotFoundException(nameof(VehicleModel), request.Id);

        mapper.Map(request, vehicleModel);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private Task<VehicleModel?> GetVehicleModelAsync(Guid id)
    {
        return dbContext.VehicleModels
                    .AsTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
    }
}
