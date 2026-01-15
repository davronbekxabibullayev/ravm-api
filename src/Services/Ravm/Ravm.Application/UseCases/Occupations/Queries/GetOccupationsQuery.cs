namespace Ravm.Application.UseCases.Occupations.Queries;

using Ravm.Application.UseCases.Occupations.Models;

public record GetOccupationsQuery : FilteringRequest, IRequest<PagedList<OccupationModel>>;

internal sealed class GetOccupationsQueryHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<GetOccupationsQuery, PagedList<OccupationModel>>
{
    private readonly IAppDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    public async Task<PagedList<OccupationModel>> Handle(GetOccupationsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Occupations
            .AsQueryable()
            .ProjectTo<OccupationModel>(_mapper.ConfigurationProvider)
            .ToPagedListAsync(request);
    }
}
