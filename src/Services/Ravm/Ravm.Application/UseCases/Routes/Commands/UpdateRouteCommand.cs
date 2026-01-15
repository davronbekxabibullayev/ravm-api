namespace Ravm.Application.UseCases.Routes.Commands;

using Microsoft.EntityFrameworkCore;
using Ravm.Domain.Enums;

public record UpdateRouteCommand(
    Guid Id,
    string Name,
    string NameRu,
    string? NameUz,
    string? NameKa,
    string? Number,
    Guid RouteClassificationId,
    double Distance,
    double TripDuration,
    RouteSeason RouteSeason,
    DateTimeOffset RouteOpenedDate,
    string? Note,
    int RouteVehicleAmount,
    int? BackRouteVehicleAmount,
    Guid OrganizationId) : IRequest
{
    public ICollection<CreateStopPointModel> StopPoints { get; set; } = Array.Empty<CreateStopPointModel>();
}

public class UpdateRouteCommandHandler(IAppDbContext appDbContext,
    IMapper mapper) : IRequestHandler<UpdateRouteCommand>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task Handle(UpdateRouteCommand request, CancellationToken cancellationToken)
    {
        var routeId = request.Id;
        var route = await GetRouteAsync(routeId)
            ?? throw new NotFoundException(nameof(Route), routeId);

        _mapper.Map(request, route);

        await SetStopPointsAsync(route, request.StopPoints);

        await _appDbContext.SaveChangesAsync(cancellationToken);
    }

    private static void UpdateStopPoint(StopPoint stopPoint, CreateStopPointModel model)
    {
        stopPoint.Name = model.Name;
        stopPoint.NameKa = model.NameKa;
        stopPoint.NameUz = model.NameUz;
        stopPoint.NameRu = model.NameRu;
        stopPoint.Position = model.Position;
    }

    private async Task<StopPoint> NewEntityOrThrowAsync(CreateStopPointModel model)
    {
        if (await _appDbContext.StopPoints.AnyAsync(x => x.Code == model.Code))
        {
            throw new AlreadyExistsException(nameof(StopPoint), model.Code);
        }

        return new StopPoint
        {
            Name = model.Name,
            NameKa = model.NameKa,
            NameUz = model.NameUz,
            NameRu = model.NameRu,
            Code = model.Code,
            Position = model.Position
        };
    }

    private async Task SetStopPointsAsync(Route routeDb, ICollection<CreateStopPointModel> stopPointsRequest)
    {
        foreach (var routeStopPointDb in routeDb.RouteStopPoints)
        {
            var stopPointsRequestIds = stopPointsRequest.Where(a => a.Id != null).Select(a => a.Id);

            var isExist = stopPointsRequestIds.Contains(routeStopPointDb.StopPointId);

            if (!isExist)
            {
                routeDb.RouteStopPoints.Remove(routeStopPointDb);
            }
        }

        foreach (var stopPoint in stopPointsRequest)
        {
            var stopPointId = stopPoint.Id;

            if (stopPointId.HasValue)
            {
                var stopPointRequests = routeDb.RouteStopPoints.Select(x => x.StopPoint);

                var pointDb = _appDbContext.StopPoints
                    .AsTracking()
                    .FirstOrDefault(x => x.Id == stopPointId);

                if (pointDb == null)
                {
                    routeDb.RouteStopPoints.Add(new RouteStopPoint
                    {
                        StopPoint = await NewEntityOrThrowAsync(stopPoint)
                    });
                }
                else if (stopPointRequests.Any(x => x!.Id == stopPointId))
                {
                    UpdateStopPoint(pointDb, stopPoint);
                }
                else
                {
                    routeDb.RouteStopPoints.Add(new RouteStopPoint
                    {
                        StopPointId = stopPointId.Value
                    });
                }
            }
            else
            {
                routeDb.RouteStopPoints.Add(new RouteStopPoint
                {
                    StopPoint = await NewEntityOrThrowAsync(stopPoint)
                });
            }
        }
    }

    private Task<Route?> GetRouteAsync(Guid id)
    {
        return _appDbContext.Routes
            .AsTracking()
            .Include(x => x.RouteStopPoints)
             .ThenInclude(x => x.StopPoint)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
