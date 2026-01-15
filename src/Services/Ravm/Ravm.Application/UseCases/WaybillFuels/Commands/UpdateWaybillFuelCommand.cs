namespace Ravm.Application.UseCases.WaybillFuels.Commands;

using Microsoft.EntityFrameworkCore;
using Ravm.Domain.Enums;

public record UpdateWaybillFuelCommand(
Guid Id,
FundingSource FundingSource,
string RefuellerFullName,
DateTimeOffset RefuelDate,
string FuelMark,
FuelType FuelType,
double Amount,
double Price,
Guid WaybillId,
Guid WaybillDetailId) : IRequest;


internal class UpdateWaybillFuelCommandHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateWaybillFuelCommand>
{
    public async Task Handle(UpdateWaybillFuelCommand request, CancellationToken cancellationToken)
    {
        var waybillFuel = await GetWaybillFuelAsync(request.Id)
            ?? throw new NotFoundException(nameof(WaybillFuel), request.Id);

        mapper.Map(request, waybillFuel);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private Task<WaybillFuel?> GetWaybillFuelAsync(Guid id)
    {
        return dbContext.WaybillFuels
                            .AsTracking()
                            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
