namespace Ravm.Domain.Models;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<Guid>
{
    /// <summary>
    /// Аватар
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// Идентификатор сотрудника
    /// </summary>
    public Guid? EmployeeId { get; set; }

    /// <summary>
    /// Идентификатор организации
    /// </summary>
    public Guid? OrganizationId { get; set; }

    /// <summary>
    /// Удален или нет
    /// </summary>
    public bool IsDeleted { get; set; }
}
