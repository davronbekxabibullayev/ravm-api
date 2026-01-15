namespace Ravm.Domain.Models;

using Ravm.Domain.Common;

/// <summary>
/// Путевой лист
/// </summary>
public class Waybill : AuditableEntity, IHasOrganization, IDeletable
{
    public Waybill()
    {
        WaybillDetails = new HashSet<WaybillDetail>();
        WaybillFuels = new HashSet<WaybillFuel>();
        WaybillTasks = new HashSet<WaybillTask>();
        WaybillDrivers = new HashSet<WaybillDriver>();
    }

    /// <summary>
    /// Удален или нет
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Номер путевого листа
    /// </summary>
    public required string Number { get; set; }

    /// <summary>
    /// Идентификатор организации
    /// </summary>
    public Guid OrganizationId { get; set; }

    /// <summary>
    /// Организация
    /// </summary>
    public virtual Organization? Organization { get; set; }

    /// <summary>
    /// Действителен до
    /// </summary>
    public DateTimeOffset ExpireDate { get; set; }

    /// <summary>
    /// Дата начало
    /// </summary>
    public DateTimeOffset BeginDate { get; set; }

    /// <summary>
    /// Идентификатор маршрута путевого листа
    /// </summary>
    public Guid? RouteId { get; set; }

    /// <summary>
    /// Маршрут путевого листа
    /// </summary>
    public Route? Route { get; set; }

    /// <summary>
    /// Заправка топливо по путевому листу
    /// </summary>
    public ICollection<WaybillFuel> WaybillFuels { get; set; }

    /// <summary>
    /// Задачи путевого листа
    /// </summary>
    public ICollection<WaybillTask> WaybillTasks { get; set; }

    /// <summary>
    /// Детали путевого листа
    /// </summary>
    public ICollection<WaybillDetail> WaybillDetails { get; set; }

    public ICollection<WaybillDriver> WaybillDrivers { get; set; }

    /// <summary>
    /// Идентификатор ТС (транспортное средство)
    /// </summary>
    public Guid VehicleId { get; set; }

    /// <summary>
    /// ТС (транспортное средство)
    /// </summary>
    public virtual Vehicle? Vehicle { get; set; }
}
