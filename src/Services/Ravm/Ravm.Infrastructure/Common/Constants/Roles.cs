namespace Ravm.Infrastructure.Common.Constants;

public static class RoleNames
{
    public const string Admin = "Администратор Глобальный";
    public const string OrganizationAdmin = "Администратор организации";
    public const string Dispatcher = "Диспетчер";
    public const string Doctor = "Доктор";
    public const string Mechanic = "Механик";
    public const string Driver = "Водитель";
}

public static class Roles
{
    public static readonly RoleInfo Admin = new(Guid.Parse("606AC764-3EF8-49B0-9430-5BA92F4142EA"), nameof(RoleNames.Admin));
    public static readonly RoleInfo OrganizationAdmin = new(Guid.Parse("0A225566-EAE8-4D40-93F6-63BC5FCAE445"), nameof(RoleNames.OrganizationAdmin));
    public static readonly RoleInfo Dispatcher = new(Guid.Parse("0130BBE8-71C9-4A02-B744-69F2FF60AC76"), nameof(RoleNames.Dispatcher));
    public static readonly RoleInfo Doctor = new(Guid.Parse("7BFF6554-1135-46A4-B25B-15DBEC8B052F"), nameof(RoleNames.Doctor));
    public static readonly RoleInfo Mechanic = new(Guid.Parse("B280474C-3BF9-4503-9CF4-5FC7AE27A8B5"), nameof(RoleNames.Mechanic));
    public static readonly RoleInfo Driver = new(Guid.Parse("D1261E68-DCFF-407D-A657-3EC7B4A1153C"), nameof(RoleNames.Driver));

    public static List<RoleInfo> List()
    {
        var list = typeof(Roles)
            .GetFields()
            .Where(x => x.IsStatic && x.IsPublic && x.FieldType == typeof(RoleInfo))
            .Select(x => x.GetValue(null))
            .OfType<RoleInfo>()
            .ToList();

        return list;
    }
}

public readonly struct RoleInfo
{
    public RoleInfo(Guid id, string name)
    {
        Id = id;
        Name = name;
        NormilizedName = name.ToUpper(System.Globalization.CultureInfo.CurrentCulture);
    }

    public Guid Id { get; }
    public string Name { get; }
    public string NormilizedName { get; }
}
