namespace Ravm.Application.UseCases.WaybillFuels.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.UseCases.WaybillFuels.Models;

public record GetWaybillFuelQuery(Guid Id) : IRequest<WaybillFuelModel>;

internal sealed class GetWaybillFuelQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetWaybillFuelQuery, WaybillFuelModel>
{
    public async Task<WaybillFuelModel> Handle(GetWaybillFuelQuery request, CancellationToken cancellationToken)
    {
        var waybillFuel = await dbContext.WaybillFuels
            .FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken)
            ?? throw new NotFoundException(nameof(WaybillFuel), request.Id);

        return mapper.Map<WaybillFuelModel>(waybillFuel);
    }
}

