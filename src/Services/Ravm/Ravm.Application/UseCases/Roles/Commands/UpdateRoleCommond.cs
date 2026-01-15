namespace Ravm.Application.UseCases.Roles.Commands;

public record UpdateRoleCommond
{
    public UpdateRoleCommond()
    {
        Permissions = new HashSet<string>();
    }
    public Guid Id { get; init; } = default!;
    public string? Name { get; init; }
    public IEnumerable<string> Permissions { get; set; }

}
