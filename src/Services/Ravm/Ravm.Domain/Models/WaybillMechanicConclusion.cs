namespace Ravm.Domain.Models;

using Ravm.Domain.Common;
using Ravm.Domain.Enums;

/// <summary>
/// Заключения механик по путевой листа
/// </summary>
public class WaybillMechanicConclusion : AuditableEntity, IDeletable
{

    /// <summary>
    /// Идентификатор механика
    /// </summary>
    public Guid MechanicId { get; set; }

    public virtual Employee? Mechanic { get; set; }

    /// <summary>
    /// Детали путевого листа идентификатора
    /// </summary>
    public Guid WaybillDetailId { get; set; }

    /// <summary>
    /// Детали путевого листа
    /// </summary>
    public virtual WaybillDetail? WaybillDetail { get; set; }

    /// <summary>
    /// Идентификатор принимающего водитель
    /// </summary>
    public Guid? ReceivedDriverId { get; set; }

    /// <summary>
    /// Принимающий водитель
    /// </summary>
    public virtual Employee? ReceivedDriver { get; set; }

    /// <summary>
    /// Идентификатор возвращаюшего водителья
    /// </summary>
    public Guid? ReturnedDriverId { get; set; }

    /// <summary>
    /// Возвращающий водитель
    /// </summary>
    public virtual Employee? ReturnedDriver { get; set; }

    /// <summary>
    /// Двигатель здоров
    /// </summary>
    public bool IsEngineHealthy { get; set; }

    /// <summary>
    /// Шина здорова
    /// </summary>
    public bool IsTireHealthy { get; set; }

    /// <summary>
    /// Тормоза исправны
    /// </summary>
    public bool IsBrakeHealthy { get; set; }

    /// <summary>
    /// Здоровая ли передача
    /// </summary>
    public bool IsTransmissionHealthy { get; set; }

    /// <summary>
    /// Примечание
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Прием и сдача механического заключения
    /// </summary>
    public MechanicConclusionType MechanicConclusionType { get; set; }

    /// <summary>
    /// Исправен ли автомобиль или нет
    /// </summary>
    public bool IsVehicleHealthy { get; set; }

    /// <summary>
    /// Удален или нет
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Показание спидометра в получении ТС
    /// </summary>
    public double SpeedometerIndication { get; set; }

    /// <summary>
    /// Показание спидометра в возврате ТС
    /// </summary>
    public double ReturnSpeedometer { get; set; }

    /// <summary>
    /// Количество топлива ТС
    /// </summary>
    public double FuelAmount { get; set; }
}
