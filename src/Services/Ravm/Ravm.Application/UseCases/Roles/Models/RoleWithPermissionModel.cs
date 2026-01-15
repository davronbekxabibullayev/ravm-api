namespace Ravm.Application.UseCases.RouteClassifications.Models;

public record RoleWithPermissionModel
{
    public RoleWithPermissionModel()
    {
        Permissions = new HashSet<PermissionInfoModel>();
    }
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public ICollection<PermissionInfoModel> Permissions { get; set; }
}

public struct PermissionInfoModel
{
    public string Key { get; set; }
    public string DisplayName { get; set; }
    public string DisplayNameRu { get; set; }
    public string DisplayNameKa { get; set; }
    public string DisplayNameEn { get; set; }
    public string Group { get; set; }
}
