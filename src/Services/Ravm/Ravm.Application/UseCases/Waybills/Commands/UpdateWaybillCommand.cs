namespace Ravm.Application.UseCases.Waybills.Commands;

using Microsoft.EntityFrameworkCore;

public record UpdateWaybillCommand
(
Guid Id,
string Number,
Guid OrganizationId,
DateTimeOffset ExpireDate,
DateTimeOffset BeginDate,
Guid? RouteId,
Guid VehicleId,
ICollection<Guid>? DriverIds) : IRequest;

internal class UpdateWaybillCommandHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<UpdateWaybillCommand>
{
    public async Task Handle(UpdateWaybillCommand command, CancellationToken cancellationToken)
    {
        var waybill = await GetWaybillAsync(command.Id)
            ?? throw new NotFoundException(nameof(Waybill), command.Id);

        if (command.DriverIds != null && command.DriverIds.Count > 0)
        {
            foreach (var employeeId in command.DriverIds)
            {
                if (!waybill.WaybillDrivers.Any(driver => driver.EmployeeId == employeeId))
                {
                    waybill.WaybillDrivers.Add(new()
                    {
                        EmployeeId = employeeId
                    });
                }
            }

            foreach (var driver in waybill.WaybillDrivers.ToList())
            {
                if (!command.DriverIds.Contains(driver.EmployeeId))
                {
                    waybill.WaybillDrivers.Remove(driver);
                }
            }
        }
        else
        {
            waybill.WaybillDrivers.Clear();
        }

        mapper.Map(command, waybill);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private Task<Waybill?> GetWaybillAsync(Guid id)
    {
        return dbContext.Waybills
            .AsTracking()
            .Include(x => x.WaybillDrivers)
            .SingleOrDefaultAsync(x => x.Id == id);
    }
}
