namespace Ravm.Application.UseCases.RouteStopPoints.Commands;

using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public record DeleteRouteStopPointCommand(Guid RouteId, Guid StopPointId) : IRequest;

public class DeleteRouteStopPointCommandHandler(
    IAppDbContext appDbContext) : IRequestHandler<DeleteRouteStopPointCommand>
{
    private readonly IAppDbContext _appDbContext = appDbContext;

    public async Task Handle(DeleteRouteStopPointCommand request, CancellationToken cancellationToken)
    {
        var routeStopPoint = await GetRouteStopPointAsync(request.RouteId, request.StopPointId) ??
            throw new NotFoundException("Не найдена точка остановки");

        _appDbContext.RouteStopPoints.Remove(routeStopPoint);
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }

    private Task<RouteStopPoint?> GetRouteStopPointAsync(Guid routeId, Guid stopPointId)
    {
        return _appDbContext.RouteStopPoints
            .FirstOrDefaultAsync(x => x.RouteId == routeId && x.StopPointId == stopPointId);
    }
}
