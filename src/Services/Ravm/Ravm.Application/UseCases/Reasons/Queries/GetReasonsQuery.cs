namespace Ravm.Application.UseCases.Reasons.Queries;

using Ravm.Application.UseCases.Reasons.Models;

public record GetReasonsQuery() : FilteringRequest, IRequest<PagedList<ReasonModel>>;

internal sealed class GetReasonsQueryHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<GetReasonsQuery, PagedList<ReasonModel>>
{
    public async Task<PagedList<ReasonModel>> Handle(GetReasonsQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Reasons
            .AsQueryable()
            .ProjectTo<ReasonModel>(mapper.ConfigurationProvider)
            .ToPagedListAsync(request);
    }
}

