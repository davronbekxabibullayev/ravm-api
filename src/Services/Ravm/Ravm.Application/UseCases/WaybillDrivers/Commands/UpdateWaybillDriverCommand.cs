namespace Ravm.Application.UseCases.WaybillDrivers.Commands;

using Microsoft.EntityFrameworkCore;
using Ravm.Domain.Enums;

public record UpdateWaybillDriverCommand
(
Guid Id,
Guid EmployeeId,
Guid WaybillId,
WaybillDriverRole? WaybillDriverRole) : IRequest;

internal class UpdateWaybillDriverCommandHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<UpdateWaybillDriverCommand>
{
    public async Task Handle(UpdateWaybillDriverCommand request, CancellationToken cancellationToken)
    {
        var waybillDriver = await GetWaybillDriverAsync(request.Id)
            ?? throw new NotFoundException(nameof(WaybillDriver), request.Id);

        mapper.Map(request, waybillDriver);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private Task<WaybillDriver?> GetWaybillDriverAsync(Guid id)
    {
        return dbContext.WaybillDrivers
            .AsTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
