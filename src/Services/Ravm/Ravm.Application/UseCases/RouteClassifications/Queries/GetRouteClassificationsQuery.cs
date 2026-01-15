namespace Ravm.Application.UseCases.RouteClassifications.Queries;

using Ravm.Application.UseCases.RouteClassifications.Models;

public record GetRouteClassificationsQuery : FilteringRequest, IRequest<PagedList<RouteClassificationModel>>;

internal sealed class GetRouteClassificationsQueryHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<GetRouteClassificationsQuery, PagedList<RouteClassificationModel>>
{
    private readonly IAppDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<PagedList<RouteClassificationModel>> Handle(GetRouteClassificationsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.RouteClassifications
            .AsQueryable()
            .ProjectTo<RouteClassificationModel>(_mapper.ConfigurationProvider)
            .ToPagedListAsync(request);
    }
}

