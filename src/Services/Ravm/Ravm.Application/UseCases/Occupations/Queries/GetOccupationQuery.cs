namespace Ravm.Application.UseCases.Occupations.Queries;
using Microsoft.EntityFrameworkCore;
using Ravm.Application.UseCases.Occupations.Models;
using Domain.Models;

public record GetOccupationQuery(Guid id) : IRequest<OccupationModel>;

internal sealed class GetOccupationQueryHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<GetOccupationQuery, OccupationModel>
{
    private readonly IAppDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<OccupationModel> Handle(GetOccupationQuery query, CancellationToken cancellationToken)
    {
        var occupation = await _dbContext.Occupations.FirstOrDefaultAsync(x => x.Id == query.id, cancellationToken)
            ?? throw new NotFoundException(nameof(Occupation), query.id);

        return _mapper.Map<OccupationModel>(occupation);
    }
}
