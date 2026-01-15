namespace Ravm.Application.UseCases.Routes.Queries;

using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ravm.Application.UseCases.Routes.Models;

public record GetRouteQuery(Guid Id) : IRequest<RouteWithDetailsModel>;

public class GetRouteQueryHandler(IAppDbContext appDbContext,
    IMapper mapper) : IRequestHandler<GetRouteQuery, RouteWithDetailsModel>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<RouteWithDetailsModel> Handle(GetRouteQuery request, CancellationToken cancellationToken)
    {
        var routeId = request.Id;
        var route = await GetRouteAsync(routeId)
            ?? throw new NotFoundException(nameof(Route), routeId);

        return _mapper.Map<RouteWithDetailsModel>(route);
    }

    private Task<Route?> GetRouteAsync(Guid id)
    {
        return _appDbContext.Routes
            .AsTracking()
            .Include(x => x.Organization)
            .Include(x => x.RouteStopPoints)
             .ThenInclude(x => x.StopPoint)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
