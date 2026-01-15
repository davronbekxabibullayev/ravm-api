namespace Ravm.Application.UseCases.WaybillFuels.Queries;

using Ravm.Application.UseCases.WaybillFuels.Models;

public record GetWaybillFuelsQuery(Guid? WaybillId) : FilteringRequest, IRequest<PagedList<WaybillFuelModel>>;

internal sealed class GetWaybillFuelsQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetWaybillFuelsQuery, PagedList<WaybillFuelModel>>
{
    public async Task<PagedList<WaybillFuelModel>> Handle(GetWaybillFuelsQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.WaybillFuels.AsQueryable();

        if (request.WaybillId.HasValue)
        {
            query = query.Where(x => x.WaybillId == request.WaybillId);
        }

        return await query
            .ProjectTo<WaybillFuelModel>(mapper.ConfigurationProvider)
            .ToPagedListAsync(request);
    }
}
