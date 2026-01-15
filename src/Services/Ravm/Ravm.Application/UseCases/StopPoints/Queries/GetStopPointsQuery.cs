namespace Ravm.Application.UseCases.StopPoints.Queries;

using Ravm.Application.UseCases.StopPoints.Models;

public record GetStopPointsQuery : FilteringRequest, IRequest<PagedList<StopPointModel>>;

internal class GetRouteStopsQueryHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<GetStopPointsQuery, PagedList<StopPointModel>>
{
    private readonly IAppDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<PagedList<StopPointModel>> Handle(GetStopPointsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.StopPoints
            .AsQueryable()
            .ProjectTo<StopPointModel>(_mapper.ConfigurationProvider)
            .ToPagedListAsync(request);
    }
}
