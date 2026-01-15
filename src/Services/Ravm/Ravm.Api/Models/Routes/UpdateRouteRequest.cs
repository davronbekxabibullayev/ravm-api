namespace Ravm.Api.Models.Routes;

using Ravm.Application.UseCases.Routes.Commands;
using Ravm.Domain.Enums;

public record UpdateRouteRequest
{
    public required string Name { get; init; }
    public required string NameRu { get; init; }
    public string? NameUz { get; init; }
    public string? NameKa { get; init; }
    public string? Number { get; init; }
    public Guid RouteClassificationId { get; init; }
    public double Distance { get; init; }
    public double TripDuration { get; init; }
    public RouteSeason RouteSeason { get; init; }
    public DateTimeOffset RouteOpenedDate { get; init; }
    public string? Note { get; init; }
    public int RouteVehicleAmount { get; init; }
    public int? BackRouteVehicleAmount { get; init; }
    public Guid OrganizationId { get; init; }
    public ICollection<CreateStopPointModel> StopPoints { get; init; } = Array.Empty<CreateStopPointModel>();
}
