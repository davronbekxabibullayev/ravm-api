namespace Ravm.Application.UseCases.WaybillDrivers.Queries;

using Ravm.Application.UseCases.WaybillDrivers.Models;

public record GetWaybillDriversQuery(Guid? WaybillId) : FilteringRequest, IRequest<PagedList<WaybillDriverModel>>;

internal sealed class GetWaybillDriversQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetWaybillDriversQuery, PagedList<WaybillDriverModel>>
{
    public async Task<PagedList<WaybillDriverModel>> Handle(GetWaybillDriversQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.WaybillDrivers.AsQueryable();

        if (request.WaybillId.HasValue)
        {
            query = query.Where(x => x.WaybillId == request.WaybillId);
        }

        return await query
            .ProjectTo<WaybillDriverModel>(mapper.ConfigurationProvider)
            .ToPagedListAsync(request);
    }
}
