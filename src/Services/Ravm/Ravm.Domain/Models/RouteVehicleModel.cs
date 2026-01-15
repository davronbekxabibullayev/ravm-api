namespace Ravm.Domain.Models;

using Ravm.Domain.Common;

/// <summary>
/// Модели ТС Участвующие в маршруте
/// </summary>
public class RouteVehicleModel : Entity
{
    /// <summary>
    /// Идентификатор модели
    /// </summary>
    public Guid VehicleModelId { get; set; }

    /// <summary>
    /// Идентификатор маршрута
    /// </summary>
    public Guid RouteId { get; set; }

    /// <summary>
    /// Модель ТС
    /// </summary>
    public virtual VehicleModel? VehicleModel { get; set; }

    /// <summary>
    /// Маршрут
    /// </summary>
    public virtual Route? Route { get; set; }
}
