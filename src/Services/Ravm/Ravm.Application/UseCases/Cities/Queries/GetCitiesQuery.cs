namespace Ravm.Application.UseCases.Cities.Queries;

using Ravm.Application.UseCases.Cities.Models;

public record GetCitiesQuery(Guid? RegionId) : FilteringRequest, IRequest<PagedList<CityModel>>;

internal sealed class GetCitiesQueryHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<GetCitiesQuery, PagedList<CityModel>>
{
    public async Task<PagedList<CityModel>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.Cities.AsQueryable();

        if (request.RegionId.HasValue)
        {
            query = query.Where(w => w.RegionId == request.RegionId);
        }

        return await query
            .ProjectTo<CityModel>(mapper.ConfigurationProvider)
            .ToPagedListAsync(request);
    }
}
