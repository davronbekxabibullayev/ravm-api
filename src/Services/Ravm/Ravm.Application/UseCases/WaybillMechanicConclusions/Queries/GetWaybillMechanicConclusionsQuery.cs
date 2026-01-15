namespace Ravm.Application.UseCases.WaybillMechanicConclusions.Queries;

using Ravm.Application.UseCases.WaybillMechanicConclusions.Models;
using Ravm.Domain.Enums;

public record GetWaybillMechanicConclusionsQuery(Guid? MechanicId, Guid? DetailId, MechanicConclusionType? ConclusionType)
    : FilteringRequest, IRequest<PagedList<WaybillMechanicConclusionModel>>;

internal sealed class GetWaybillMechanicConclusionsQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetWaybillMechanicConclusionsQuery, PagedList<WaybillMechanicConclusionModel>>
{
    public async Task<PagedList<WaybillMechanicConclusionModel>> Handle(GetWaybillMechanicConclusionsQuery request,
        CancellationToken cancellationToken)
    {
        var query = dbContext.WaybillMechanicConclusions.AsQueryable();

        if (request.MechanicId.HasValue)
        {
            query = query.Where(x => x.MechanicId == request.MechanicId);
        }

        if (request.DetailId.HasValue)
        {
            query = query.Where(x => x.WaybillDetailId == request.DetailId);
        }

        if (request.ConclusionType.HasValue)
        {
            query = query.Where(x => x.MechanicConclusionType == request.ConclusionType);
        }

        return await query
            .ProjectTo<WaybillMechanicConclusionModel>(mapper.ConfigurationProvider)
            .ToPagedListAsync(request);
    }
}
