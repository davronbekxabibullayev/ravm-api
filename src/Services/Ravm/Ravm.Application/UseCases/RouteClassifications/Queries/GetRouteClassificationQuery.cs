namespace Ravm.Application.UseCases.RouteClassifications.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.UseCases.RouteClassifications.Models;

public record GetRouteClassificationQuery(Guid Id) : IRequest<RouteClassificationModel>;

internal class GetRouteClassificationQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetRouteClassificationQuery, RouteClassificationModel>
{
    private readonly IAppDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<RouteClassificationModel> Handle(GetRouteClassificationQuery request, CancellationToken cancellationToken)
    {
        var routeClassification = await _dbContext.RouteClassifications
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(RouteClassification), request.Id);

        return _mapper.Map<RouteClassificationModel>(routeClassification);
    }
}

