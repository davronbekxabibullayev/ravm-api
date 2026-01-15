namespace Ravm.Application.UseCases.WaybillDrivers.Commands;

using Microsoft.EntityFrameworkCore;
using Ravm.Domain.Enums;

public record CreateWaybillDriverCommand : IRequest
{
    public Guid EmployeeId { get; set; }
    public Guid WaybillId { get; set; }
    public WaybillDriverRole? WaybillDriverRole { get; set; }
}

internal class CreateWaybillDriverCommandHandler(IAppDbContext dbContext)
    : IRequestHandler<CreateWaybillDriverCommand>
{
    public async Task Handle(CreateWaybillDriverCommand request, CancellationToken cancellationToken)
    {

        var waybillDriver = await dbContext.WaybillDrivers
            .FirstOrDefaultAsync(x => x.WaybillId.Equals(request.WaybillId)
            && x.EmployeeId.Equals(request.EmployeeId), cancellationToken);

        if (waybillDriver != null)
            throw new AlreadyExistsException($"Waybill driver already exists for this employee and waybill.");

        waybillDriver = NewWaybillDriver(request);

        await dbContext.WaybillDrivers.AddAsync(waybillDriver, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static WaybillDriver NewWaybillDriver(CreateWaybillDriverCommand request)
    {
        return new WaybillDriver
        {
            EmployeeId = request.EmployeeId,
            WaybillId = request.WaybillId,
            WaybillDriverRole = request.WaybillDriverRole
        };
    }
}
