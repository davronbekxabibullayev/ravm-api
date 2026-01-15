namespace Ravm.Application.UseCases.Localities.Queries;

using Ravm.Application.UseCases.Localities.Models;

public record GetLocalitiesQuery(Guid? RegionId, Guid? CityId) : FilteringRequest, IRequest<PagedList<LocalityModel>>;

internal sealed class GetLocalitiesQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetLocalitiesQuery, PagedList<LocalityModel>>
{
    private readonly IAppDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<PagedList<LocalityModel>> Handle(GetLocalitiesQuery request, CancellationToken cancellationToken)
    {
        var loclities = _dbContext.Localities.AsQueryable();

        if (request.RegionId.HasValue)
        {
            loclities = loclities.Where(x => x.RegionId == request.RegionId);
        }
        if (request.CityId.HasValue)
        {
            loclities = loclities.Where(x => x.CityId == request.CityId);
        }

        return await loclities.ProjectTo<LocalityModel>(_mapper.ConfigurationProvider).ToPagedListAsync(request);
    }
}
