namespace Ravm.Domain.Models;

using Ravm.Domain.Common;
using Ravm.Domain.Enums;

/// <summary>
/// Адрес организации
/// </summary>
public class OrganizationAddress : Entity, IHasOrganization, IDeletable
{
    /// <summary>
    /// Адрес: район улица дом
    /// </summary>
    public required string AddressLine1 { get; set; }

    /// <summary>
    /// Адрес: дом квартира этаж
    /// </summary>
    public string? AddressLine2 { get; set; }

    /// <summary>
    /// Тип адреса
    /// </summary>
    public AddressType Type { get; set; }

    /// <summary>
    /// Идентификатор города/района
    /// </summary>
    public Guid CityId { get; set; }

    /// <summary>
    /// Идентификатор региона
    /// </summary>
    public Guid RegionId { get; set; }

    /// <summary>
    /// Долгота (WGS 84)
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Широта (WGS 84)
    /// </summary>
    public double Latitude { get; set; }

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
