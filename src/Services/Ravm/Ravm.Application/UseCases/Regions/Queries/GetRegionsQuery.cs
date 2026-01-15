namespace Ravm.Application.UseCases.Regions.Queries;

using System.Threading;
using System.Threading.Tasks;
using Ravm.Application.UseCases.Regions.Models;

public record GetRegionsQuery(Guid? CountryId) : FilteringRequest, IRequest<PagedList<RegionModel>>;

internal sealed class GetRegionsQueryHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<GetRegionsQuery, PagedList<RegionModel>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IAppDbContext _dbContext = dbContext;

    public async Task<PagedList<RegionModel>> Handle(GetRegionsQuery request, CancellationToken cancellationToken)
    {
        var regions = _dbContext.Regions.AsQueryable();

        if (request.CountryId.HasValue)
        {
            regions = regions.Where(x => x.CountryId == request.CountryId);
        }

        return await regions.ProjectTo<RegionModel>(_mapper.ConfigurationProvider).ToPagedListAsync(request);
    }
}
