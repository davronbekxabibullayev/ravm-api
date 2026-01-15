namespace Ravm.Domain.Models;

using Ravm.Domain.Common;
using Ravm.Domain.Enums;

/// <summary>
/// Маршрут
/// </summary>
public class Route : LocalizableAuditableEntity, IHasOrganization, IDeletable
{
    public Route()
    {
        RouteStopPoints = new HashSet<RouteStopPoint>();
    }

    /// <summary>
    /// Номер маршрута
    /// </summary>
    public string? Number { get; set; }

    /// <summary>
    /// Удален или нет
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Идентификатор классификации маршрута
    /// </summary>
    public Guid RouteClassificationId { get; set; }

    /// <summary>
    /// Классификация маршрута
    /// </summary>
    public virtual RouteClassification? RouteClassification { get; set; }

    /// <summary>
    /// Дистанция маршрута
    /// </summary>
    public double Distance { get; set; }

    /// <summary>
    /// Продолжительность поездки в минутах
    /// </summary>
    public double TripDuration { get; set; }

    /// <summary>
    /// Сезонность маршрута
    /// </summary>
    public RouteSeason RouteSeason { get; set; }

    /// <summary>
    /// Дата открытие маршрута
    /// </summary>
    public DateTimeOffset RouteOpenedDate { get; set; }

    /// <summary>
    /// Примечание
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Количество ТС (траснпортное средство) маршрута
    /// </summary>

    public int RouteVehicleAmount { get; set; }

    /// <summary>
    /// Количество ТС (траснпортное средство) маршрута в обратном пути
    /// </summary>
    public int? BackRouteVehicleAmount { get; set; }

    /// <summary>
    /// Идентификатор организации
    /// </summary>
    public Guid OrganizationId { get; set; }
    public virtual Organization? Organization { get; set; }

    /// <summary>
    /// Остановки маршрута
    /// </summary>
    public ICollection<RouteStopPoint> RouteStopPoints { get; set; }
}
