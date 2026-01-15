namespace Ravm.Infrastructure.Common.Constants;

public readonly struct PermissionInfo
{
    public PermissionInfo(string key, string group, string displayName, string displayNameRu, string displayNameKa, string displayNameEn)
    {
        Key = key;
        DisplayName = displayName;
        DisplayNameRu = displayNameRu;
        DisplayNameKa = displayNameKa;
        DisplayNameEn = displayNameEn;
        Group = group;
    }
    public string Key { get; }
    public string DisplayName { get; }
    public string DisplayNameRu { get; }
    public string DisplayNameKa { get; }
    public string DisplayNameEn { get; }
    public string Group { get; }
}
public readonly struct Permissions
{
    public struct Admin
    {
        public const string Logs = "1001";
        public const string UserSystemSettings = "1002";
        public const string ManagementOrganization = "1003";
        public const string ManagmentOrganizationEmployees = "1004";
        public const string UserAccountsRegionalAdministratorsCreate = "1005";
        public const string ManageSettings = "1006";
        public const string ManagingRoles = "1007";
        public const string DirectoryManagement = "1008";
    }

    public struct HR
    {
        public const string ScheduleDelete = "2001";
        public const string PersonalSettingEmployee = "2002";
        public const string SelectStatuses = "2003";
        public const string UpdateEmployee = "2004";
        public const string MarkEmployee = "2005";
        public const string SearchEmployee = "2006";
    }

    public struct Doctor
    {
        public const string MenegmentDoctorConclusion = "3001";
    }

    public struct Mechanic
    {
        public const string MenegmentMechanicConclusion = "4001";
    }

    public struct Dispatcher
    {
        public const string ManagmentOrganizationTransports = "5001";
    }


    public static readonly List<PermissionInfo> List = new()
    {
        new PermissionInfo(Admin.Logs, "Администрирование", "Доступ к просмотру Лог-журнала", "Доступ к просмотру Лог-журнала", "Доступ к просмотру Лог-журнала", "Доступ к просмотру Лог-журнала"),
        new PermissionInfo(Admin.UserSystemSettings, "Администрирование", "Доступ к пользовательским настройкам системы", "Доступ к пользовательским настройкам системы", "Доступ к пользовательским настройкам системы", "Доступ к пользовательским настройкам системы"),
        new PermissionInfo(Admin.ManagementOrganization, "Администрирование", "Доступ к управлению Организации", "Доступ к управлению Организации", "Доступ к управлению Организации", "Доступ к управлению Организации"),
        new PermissionInfo(Admin.ManagmentOrganizationEmployees, "Администрирование", "Доступ к управлению сотрудников организации", "Доступ к управлению сотрудников организации", "Доступ к управлению сотрудников организации", "Доступ к управлению сотрудников организации"),
        new PermissionInfo(Admin.UserAccountsRegionalAdministratorsCreate, "Администрирование", "Досутп к создани. учётных записей пользователей – администраторов региональных", "Досутп к создани. учётных записей пользователей – администраторов региональных", "Досутп к создани. учётных записей пользователей – администраторов региональных", "Досутп к создани. учётных записей пользователей – администраторов региональных"),
        new PermissionInfo(Admin.ManageSettings, "Администрирование", "Доступ к управлению настройками ", "Доступ к управлению настройками ", "Доступ к управлению настройками ", "Доступ к управлению настройками "),
        new PermissionInfo(Admin.ManagingRoles, "Администрирование", "Доступ к управлению ролей в системе", "Доступ к управлению ролей в системе", "Доступ к управлению ролей в системе", "Доступ к управлению ролей в системе"),
        new PermissionInfo(Admin.DirectoryManagement, "Администрирование", "Доступ к управлению справочников", "Доступ к управлению справочников", "Доступ к управлению справочников", "Доступ к управлению справочников"),
        new PermissionInfo(HR.ScheduleDelete, "УКР", "Доступ к удалению план-графика (только со статусом ЧЕРНОВИК)", "Доступ к удалению план-графика (только со статусом ЧЕРНОВИК)", "Доступ к удалению план-графика (только со статусом ЧЕРНОВИК)", "Доступ к удалению план-графика (только со статусом ЧЕРНОВИК)"),
        new PermissionInfo(HR.PersonalSettingEmployee, "УКР", "Доступ к личным настройкам сотрудника (карточка сотрудника)", "Доступ к личным настройкам сотрудника (карточка сотрудника)", "Доступ к личным настройкам сотрудника (карточка сотрудника)", "Доступ к личным настройкам сотрудника (карточка сотрудника)"),
        new PermissionInfo(HR.SelectStatuses, "УКР", "Доступ к выбору статусов (смены/свободен/отпуск/праздничные дни)", "Доступ к выбору статусов (смены/свободен/отпуск/праздничные дни)", "Доступ к выбору статусов (смены/свободен/отпуск/праздничные дни)", "Доступ к выбору статусов (смены/свободен/отпуск/праздничные дни)"),
        new PermissionInfo(HR.UpdateEmployee, "УКР", "Доступ к изменению сотрудника на другого сотрудника", "Доступ к изменению сотрудника на другого сотрудника", "Доступ к изменению сотрудника на другого сотрудника", "Доступ к изменению сотрудника на другого сотрудника"),
        new PermissionInfo(HR.MarkEmployee, "УКР", "Доступ к отметке по сотруднику", "Доступ к отметке по сотруднику", "Доступ к отметке по сотруднику", "Доступ к отметке по сотруднику"),
        new PermissionInfo(HR.SearchEmployee, "УКР", "Доступ к поиску сотрудника", "Доступ к поиску сотрудника", "Доступ к поиску сотрудника", "Доступ к поиску сотрудника"),
        new PermissionInfo(Doctor.MenegmentDoctorConclusion, "Доктор", "Настроить примечания доктора", "Настроить примечания доктора", "Настроить примечания доктора", "Настроить примечания доктора"),
        new PermissionInfo(Dispatcher.ManagmentOrganizationTransports, "Механик", "Досутп к управлению транспортному средству организации", "Досутп к управлению транспортному средству организации", "Досутп к управлению транспортному средству организации", "Досутп к управлению транспортному средству организации"),
        new PermissionInfo(Mechanic.MenegmentMechanicConclusion, "Механик", "Настроить примечания механика", "Настроить примечания механика", "Настроить примечания механика", "Настроить примечания механика"),
    };
}
