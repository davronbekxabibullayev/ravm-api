namespace Ravm.Domain.Models;

using Ravm.Domain.Common;

/// <summary>
/// Должность сотрудника
/// </summary>
public class EmployeeOccupation : Entity
{

    /// <summary>
    /// Идентификатор сотрудника
    /// </summary>
    public Guid EmployeeId { get; set; }

    /// <summary>
    /// Идентификатор должности
    /// </summary>
    public Guid OccupationId { get; set; }

    /// <summary>
    /// Сотрудник
    /// </summary>
    public virtual Employee? Employee { get; set; }

    /// <summary>
    /// Должность
    /// </summary>
    public virtual Occupation? Occupation { get; set; }

}
