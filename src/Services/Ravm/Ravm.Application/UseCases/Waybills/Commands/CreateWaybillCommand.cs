namespace Ravm.Application.UseCases.Waybills.Commands;

public record CreateWaybillCommand : IRequest
{
    public required string Number { get; set; }
    public Guid OrganizationId { get; set; }
    public DateTimeOffset ExpireDate { get; set; }
    public DateTimeOffset BeginDate { get; set; }
    public Guid? RouteId { get; set; }
    public Guid VehicleId { get; set; }
    public ICollection<Guid>? DriverIds { get; set; }
}

internal class CreateWaybillCommandHandler(IAppDbContext dbContext) : IRequestHandler<CreateWaybillCommand>
{
    public async Task Handle(CreateWaybillCommand request, CancellationToken cancellationToken)
    {
        var waybill = NewWaybill(request);

        if (request.DriverIds != null && request.DriverIds.Count > 0)
        {
            foreach (var employeeId in request.DriverIds)
            {
                waybill.WaybillDrivers.Add(new WaybillDriver
                {
                    EmployeeId = employeeId
                });
            }
        }

        await dbContext.Waybills.AddAsync(waybill, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static Waybill NewWaybill(CreateWaybillCommand command)
    {
        return new Waybill
        {
            Number = command.Number,
            OrganizationId = command.OrganizationId,
            ExpireDate = command.ExpireDate,
            BeginDate = command.BeginDate,
            RouteId = command.RouteId,
            VehicleId = command.VehicleId
        };
    }
}
