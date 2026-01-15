namespace Ravm.Application.UseCases.Regions.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.UseCases.Regions.Models;

public record GetRegionQuery(Guid Id) : IRequest<RegionModel>;

internal sealed class GetRegionQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetRegionQuery, RegionModel>
{
    private readonly IAppDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<RegionModel> Handle(GetRegionQuery request, CancellationToken cancellationToken)
    {
        var region = await _dbContext.Regions
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(City), request.Id);

        return _mapper.Map<RegionModel>(region);
    }
}
