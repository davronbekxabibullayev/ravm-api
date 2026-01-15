namespace Ravm.Domain.Models;

using Ravm.Domain.Common;

/// <summary>
/// Транспортное средство
/// </summary>
public class Vehicle : Entity, IHasOrganization, IDeletable
{
    /// <summary>
    /// Идентификатор организации
    /// </summary>
    public Guid OrganizationId { get; set; }
    /// <summary>
    /// Организация
    /// </summary>
    public virtual Organization? Organization { get; set; }

    /// <summary>
    /// Идентификатор модели ТС
    /// </summary>
    public Guid VehicleModelId { get; set; }

    /// <summary>
    /// Модель ТС
    /// </summary>
    public virtual VehicleModel? VehicleModel { get; set; }

    /// <summary>
    /// Государственный номер ТС
    /// </summary>
    public required string StateNumber { get; set; }

    /// <summary>
    /// Гаражный номер ТС
    /// </summary>
    public required string GarageNumber { get; set; }

    /// <summary>
    /// ВИН 
    /// </summary>
    public string? Vin { get; set; }

    /// <summary>
    /// Номер шасси
    /// </summary>
    public string? ChassisNumber { get; set; }

    /// <summary>
    /// Удален или нет
    /// </summary>
    public bool IsDeleted { get; set; }
}
