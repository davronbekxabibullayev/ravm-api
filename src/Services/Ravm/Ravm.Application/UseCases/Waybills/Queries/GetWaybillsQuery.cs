namespace Ravm.Application.UseCases.Waybills.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.Extensions;
using Ravm.Application.UseCases.Waybills.Models;

public record GetWaybillsQuery(Guid? OrganizationId) : FilteringRequest, IRequest<PagedList<WaybillModel>>;

internal sealed class GetWaybillsQueryHandler(IAppDbContext dbContext, IMapper mapper, ICurrentUser currentUser)
    : IRequestHandler<GetWaybillsQuery, PagedList<WaybillModel>>
{
    public async Task<PagedList<WaybillModel>> Handle(GetWaybillsQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.Waybills
            .IncludeChilds(currentUser.OrganizationId)
            .Include(a => a.Route)
            .Include(a => a.Vehicle)
            .Include(x => x.WaybillFuels)
            .Include(x => x.WaybillTasks)
            .Include(x => x.WaybillDetails)
            .Include(x => x.WaybillDrivers)
            .ThenInclude(a => a.Employee)
            .AsQueryable();

        if (request.OrganizationId.HasValue)
        {
            query = query.Where(x => x.OrganizationId.Equals(request.OrganizationId));
        }

        var result = await query.ToPagedListAsync<Waybill, WaybillModel>(request, mapper);

        return result;
    }
}
