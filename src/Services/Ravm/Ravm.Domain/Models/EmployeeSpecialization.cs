namespace Ravm.Domain.Models;

using Ravm.Domain.Common;

/// <summary>
/// Специализация сотрудника
/// </summary>
public class EmployeeSpecialization : Entity
{
    /// <summary>
    /// Идентификатор сотрудника
    /// </summary>
    public Guid EmployeeId { get; set; }

    /// <summary>
    /// Идентификатор специализация
    /// </summary>
    public Guid SpecializationId { get; set; }

    /// <summary>
    /// Сотрудник
    /// </summary>
    public Employee? Employee { get; set; }

    /// <summary>
    /// Специализация
    /// </summary>
    public Specialization? Specialization { get; set; }
}
