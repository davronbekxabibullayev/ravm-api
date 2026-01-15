namespace Ravm.Application.UseCases.Specializations.Queries;

using Ravm.Application.UseCases.Specializations.Models;

public record GetSpecializationsQuery : FilteringRequest, IRequest<PagedList<SpecializationModel>>;

internal sealed class GetSpecializationsQueryHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<GetSpecializationsQuery, PagedList<SpecializationModel>>
{
    private readonly IAppDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    public async Task<PagedList<SpecializationModel>> Handle(GetSpecializationsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Specializations
            .AsQueryable()
            .ProjectTo<SpecializationModel>(_mapper.ConfigurationProvider)
            .ToPagedListAsync(request);
    }
}
