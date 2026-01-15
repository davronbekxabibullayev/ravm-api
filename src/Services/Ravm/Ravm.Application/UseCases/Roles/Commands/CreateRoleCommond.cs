namespace Ravm.Application.UseCases.Roles.Commands;

public record CreateRoleCommond
{
    public CreateRoleCommond()
    {
        Permissions = new HashSet<string>();
    }
    public string Name { get; init; } = default!;
    public IEnumerable<string> Permissions { get; set; }

}
