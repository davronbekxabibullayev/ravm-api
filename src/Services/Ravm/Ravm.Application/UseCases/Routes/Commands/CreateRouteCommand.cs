namespace Ravm.Application.UseCases.Routes.Commands;

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ravm.Domain.Enums;

public record CreateRouteCommand : IRequest
{
    public required string Name { get; set; }
    public required string NameRu { get; set; }
    public string? NameUz { get; set; }
    public string? NameKa { get; set; }
    public string? Number { get; set; }
    public Guid RouteClassificationId { get; set; }
    public double Distance { get; set; }
    public double TripDuration { get; set; }
    public RouteSeason RouteSeason { get; set; }
    public DateTimeOffset RouteOpenedDate { get; set; }
    public string? Note { get; set; }
    public int RouteVehicleAmount { get; set; }
    public int? BackRouteVehicleAmount { get; set; }
    public Guid OrganizationId { get; set; }
    public ICollection<CreateStopPointModel> StopPoints { get; set; } = Array.Empty<CreateStopPointModel>();
}

public class CreateRouteCommandHandler(IAppDbContext appDbContext,
    IMapper mapper) : IRequestHandler<CreateRouteCommand>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task Handle(CreateRouteCommand request, CancellationToken cancellationToken)
    {
        var route = _mapper.Map<Route>(request);

        await SetStopPointsAsync(route, request.StopPoints);

        await _appDbContext.Routes.AddAsync(route, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task SetStopPointsAsync(Route route, ICollection<CreateStopPointModel> models)
    {
        foreach (var stopPoint in models)
        {
            var stopPointId = stopPoint.Id;

            if (stopPointId.HasValue)
            {
                var point = await _appDbContext.StopPoints
                    .AsTracking()
                    .FirstOrDefaultAsync(x => x.Id == stopPointId);

                if (point == null)
                {
                    route.RouteStopPoints.Add(new RouteStopPoint
                    {
                        StopPoint = await NewStopPointOrThrowAsync(stopPoint)
                    });
                }
                else
                {
                    UpdateStopPoint(point, stopPoint);
                    route.RouteStopPoints.Add(new RouteStopPoint
                    {
                        StopPointId = stopPointId.Value
                    });
                }

            }
            else
            {
                route.RouteStopPoints.Add(new RouteStopPoint
                {
                    StopPoint = await NewStopPointOrThrowAsync(stopPoint)
                });
            }
        }
    }

    private async Task<StopPoint> NewStopPointOrThrowAsync(CreateStopPointModel model)
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

    private static void UpdateStopPoint(StopPoint stopPoint, CreateStopPointModel model)
    {
        stopPoint.Name = model.Name;
        stopPoint.NameKa = model.NameKa;
        stopPoint.NameUz = model.NameUz;
        stopPoint.NameRu = model.NameRu;
        stopPoint.Position = model.Position;
    }
}

public record CreateStopPointModel
{
    public Guid? Id { get; set; }
    public required string Name { get; set; }
    public required string NameRu { get; set; }
    public string? NameUz { get; set; }
    public string? NameKa { get; set; }
    public required string Code { get; set; }
    public StopPointPosition Position { get; set; }
}
