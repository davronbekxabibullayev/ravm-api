namespace Ravm.Application.UseCases.WaybillTasks.Queries;

using Ravm.Application.UseCases.WaybillTasks.Models;

public record GetWaybillTasksQuery(Guid? WaybillId) : FilteringRequest, IRequest<PagedList<WaybillTaskModel>>;

internal sealed class GetWaybillTasksQueryHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<GetWaybillTasksQuery, PagedList<WaybillTaskModel>>
{
    public async Task<PagedList<WaybillTaskModel>> Handle(GetWaybillTasksQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.WaybillTasks.AsQueryable();

        if (request.WaybillId.HasValue)
        {
            query = query.Where(x => x.WaybillId == request.WaybillId);
        }

        return await query
            .ProjectTo<WaybillTaskModel>(mapper.ConfigurationProvider)
            .ToPagedListAsync(request);
    }
}
