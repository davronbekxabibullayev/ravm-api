namespace Ravm.Domain.Models;
using Ravm.Domain.Common;
using Ravm.Domain.Enums;

/// <summary>
/// Сотрудник
/// </summary>
public class Employee : PersonBase, IHasOrganization, IDeletable
{
    public Employee()
    {
        EmployeeOccupations = new HashSet<EmployeeOccupation>();
        EmployeeSpecializations = new HashSet<EmployeeSpecialization>();
    }

    /// <summary>
    /// Идентификатор организации
    /// </summary>
    public Guid OrganizationId { get; set; }

    /// <summary>
    /// Организация
    /// </summary>
    public virtual Organization? Organization { get; set; }

    /// <summary>
    /// Аккаунт id
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Тип группы занятий
    /// </summary>
    public OccupationGroupType OccupationGroupType { get; set; }

    /// <summary>
    /// Штатный номер
    /// </summary>
    public required string StaffNumber { get; set; }

    /// <summary>
    /// Номер водительских прав
    /// </summary>
    public string? DriverLisenceNumber { get; set; }

    /// <summary>
    /// Удален или нет
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Должности сотрудника
    /// </summary>
    public virtual ICollection<EmployeeOccupation> EmployeeOccupations { get; set; }

    /// <summary>
    /// Специализации сотрудника
    /// </summary>
    public virtual ICollection<EmployeeSpecialization> EmployeeSpecializations { get; set; }
}
