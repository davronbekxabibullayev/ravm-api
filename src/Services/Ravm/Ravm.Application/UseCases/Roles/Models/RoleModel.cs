namespace Ravm.Application.UseCases.RouteClassifications.Models;

public record RoleModel
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
}
