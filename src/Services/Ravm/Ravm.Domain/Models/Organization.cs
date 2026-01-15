namespace Ravm.Domain.Models;

using Ravm.Domain.Common;

/// <summary>
/// Организация
/// </summary>
public class Organization : LocalizableAuditableEntity, IDeletable
{
    public Organization()
    {
        Childs = new HashSet<Organization>();
        OrganizationAddresses = new HashSet<OrganizationAddress>();
        OrganizationContacts = new HashSet<OrganizationContact>();
        Routes = new HashSet<Route>();
    }
    /// <summary>
    /// Код
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// ИНН
    /// </summary>
    public required string Tin { get; set; }

    /// <summary>
    /// ОКОНХ
    /// </summary>
    public string? Okonx { get; set; }

    /// <summary>
    /// ОКЕД
    /// </summary>
    public string? Oked { get; set; }

    /// <summary>
    /// Удален или нет
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// ID головной организация
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// Голоаная организация
    /// </summary>
    public virtual Organization? Parent { get; set; }

    /// <summary>
    /// Адреса организации
    /// </summary>
    public virtual ICollection<OrganizationAddress> OrganizationAddresses { get; set; }

    /// <summary>
    /// Контактные лица организации
    /// </summary>
    public virtual ICollection<OrganizationContact> OrganizationContacts { get; set; }

    /// <summary>
    /// Организация маршрутов
    /// </summary>
    public virtual ICollection<Route> Routes { get; set; }

    /// <summary>
    /// Филиалы/дочерные организации
    /// </summary>
    public virtual ICollection<Organization> Childs { get; set; }
}
