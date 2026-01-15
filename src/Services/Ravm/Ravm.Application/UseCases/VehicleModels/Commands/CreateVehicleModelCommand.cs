namespace Ravm.Application.UseCases.VehicleModels.Commands;

public record class CreateVehicleModelCommand : IRequest
{
    public required string Name { get; set; }
    public required string NameRu { get; set; }
    public string? NameUz { get; set; }
    public string? NameKa { get; set; }
    public Guid VehicleMarkId { get; set; }
    public required string Code { get; set; }
    public double? FuelRate { get; set; }
    public double? FuelRateWithTrailer { get; set; }
    public double? FuelRateLoaded { get; set; }
    public double? FuelRateEngineOperation { get; set; }
    public double? FuelRateLoadedEngineOperation { get; set; }
}

internal class CreateVehicleModelCommandHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<CreateVehicleModelCommand>
{
    public async Task Handle(CreateVehicleModelCommand request, CancellationToken cancellationToken)
    {
        var vehicleModel = mapper.Map<VehicleModel>(request);

        await dbContext.VehicleModels.AddAsync(vehicleModel, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
