namespace Ravm.Domain.Models;

using Ravm.Domain.Common;

/// <summary>
/// Остановки маршрута
/// </summary>
public class RouteStopPoint : Entity
{
    /// <summary>
    /// Идентификатор маршрута
    /// </summary>
    public Guid RouteId { get; set; }

    /// <summary>
    /// Идентификатор остановки
    /// </summary>
    public Guid StopPointId { get; set; }

    /// <summary>
    /// Маршрут
    /// </summary>
    public virtual Route? Route { get; set; }

    /// <summary>   
    /// Останлвка
    /// </summary>
    public virtual StopPoint? StopPoint { get; set; }
}
