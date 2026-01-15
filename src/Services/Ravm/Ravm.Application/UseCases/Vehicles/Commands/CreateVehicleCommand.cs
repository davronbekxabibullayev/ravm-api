namespace Ravm.Application.UseCases.Vehicles.Commands;

public record class CreateVehicleCommand : IRequest
{
    public Guid OrganizationId { get; set; }
    public Guid VehicleModelId { get; set; }
    public required string StateNumber { get; set; }
    public required string GarageNumber { get; set; }
    public string? Vin { get; set; }
    public string? ChassisNumber { get; set; }
}

internal class CreateVehicleCommandHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<CreateVehicleCommand>
{
    public async Task Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
    {
        var vehicle = mapper.Map<Vehicle>(request);

        await dbContext.Vehicles.AddAsync(vehicle, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
