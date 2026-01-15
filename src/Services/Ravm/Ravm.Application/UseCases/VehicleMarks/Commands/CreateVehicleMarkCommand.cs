namespace Ravm.Application.UseCases.VehicleMarks.Commands;

public record class CreateVehicleMarkCommand : IRequest
{
    public required string Name { get; set; }
    public required string NameRu { get; set; }
    public string? NameUz { get; set; }
    public string? NameKa { get; set; }
    public required string Code { get; set; }
}

internal class CreateVehicleMarkCommandHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<CreateVehicleMarkCommand>
{
    public async Task Handle(CreateVehicleMarkCommand request, CancellationToken cancellationToken)
    {
        var vehicleMark = mapper.Map<VehicleMark>(request);

        await dbContext.VehicleMarks.AddAsync(vehicleMark, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
