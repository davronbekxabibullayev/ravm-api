namespace Ravm.Application.UseCases.Routes.Models;

using Ravm.Domain.Enums;

public record RouteModel
{
    public Guid Id { get; set; }
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
}
