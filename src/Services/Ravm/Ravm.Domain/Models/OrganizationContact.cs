namespace Ravm.Domain.Models;

using Ravm.Domain.Common;

/// <summary>
/// Контактное лицо организации
/// </summary>
public class OrganizationContact : Entity, IHasOrganization, IDeletable
{
    /// <summary>
    /// ФИО контактного лица, объязательно для заполнение
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Телефон номер контактного лица, объязательно для заполнение
    /// </summary>
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Email контактного лица
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Идентификатор организации
    /// </summary>
    public Guid OrganizationId { get; set; }

    /// <summary>
    /// Организация
    /// </summary>
    public virtual Organization? Organization { get; set; }

    public bool IsDeleted { get; set; }
}
