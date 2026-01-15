namespace Ravm.Application.UseCases.Waybills.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.UseCases.Waybills.Models;

public record GetWaybillQuery(Guid Id) : IRequest<WaybillModel>;

internal sealed class GetWaybillQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetWaybillQuery, WaybillModel>
{
    public async Task<WaybillModel> Handle(GetWaybillQuery request, CancellationToken cancellationToken)
    {
        var waybill = await dbContext.Waybills
            .Include(a => a.Route)
            .Include(a => a.Vehicle)
            .Include(x => x.WaybillFuels)
            .Include(x => x.WaybillTasks)
            .Include(x => x.WaybillDetails)
            .Include(x => x.WaybillDrivers)
            .ThenInclude(a => a.Employee)
            .FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken)
            ?? throw new NotFoundException(nameof(Waybill), request.Id);

        var s = waybill.WaybillDrivers.Select(a => a.Employee);

        return mapper.Map<WaybillModel>(waybill);
    }
}
