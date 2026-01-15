namespace Ravm.Domain.Models;

using Ravm.Domain.Common;
using Ravm.Domain.Enums;

/// <summary>
/// Детали путевого листа
/// </summary>
public class WaybillDetail : Entity, IDeletable
{
    public WaybillDetail()
    {
        WaybillDoctorConclusions = new HashSet<WaybillDoctorConclusion>();
        MechanicConclusions = new HashSet<WaybillMechanicConclusion>();
        WaybillFuels = new HashSet<WaybillFuel>();
    }
    /// <summary>
    /// Удален или нет
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Идентификатор принимающего водитель
    /// </summary>
    public Guid? ReceivedDriverId { get; set; }

    /// <summary>
    /// Принимающий водитель
    /// </summary>
    public virtual Employee? ReceivedDriver { get; set; }

    /// <summary>
    /// Задачи путевого листа идентификатора
    /// </summary>
    public Guid? WaybillTaskId { get; set; }

    /// <summary>
    /// Задачи путевого листа
    /// </summary>
    public virtual WaybillTask? WaybillTask { get; set; }

    /// <summary>
    /// Идентификатор возвращаюшего водителья
    /// </summary>
    public Guid? ReturnedDriverId { get; set; }

    /// <summary>
    /// Причина идентификатора
    /// </summary>
    public Guid? IdleReasonId { get; set; }

    /// <summary>
    /// Причина
    /// </summary>
    public virtual Reason? Reason { get; set; }

    /// <summary>
    /// Возвращающий водитель
    /// </summary>
    public virtual Employee? ReturnedDriver { get; set; }

    /// <summary>
    /// Идентификатор диспетчера
    /// </summary>

    public Guid? DispatcherId { get; set; }

    /// <summary>
    /// Диспетчер
    /// </summary>
    public virtual Employee? Dispatcher { get; set; }

    /// <summary>
    /// Идентификатор менеджера
    /// </summary>
    public Guid? ManagerId { get; set; }

    /// <summary>
    /// Менеджер
    /// </summary>
    public virtual Employee? Manager { get; set; }

    /// <summary>
    /// Дата 
    /// </summary>
    public DateTimeOffset Date { get; set; }

    /// <summary>
    /// Плановое время начала
    /// </summary>
    public DateTimeOffset PlannedStartTime { get; set; }

    /// <summary>
    /// Плановое время окончания
    /// </summary>
    public DateTimeOffset PlannedEndTime { get; set; }

    /// <summary>
    /// Фактическое время начал
    /// </summary>
    public DateTimeOffset? ActualStartTime { get; set; }

    /// <summary>
    /// Фактическое время окончания
    /// </summary>
    public DateTimeOffset? ActualEndTime { get; set; }

    /// <summary>
    /// Является ли деталью по умолчанию
    /// </summary>
    public bool IsDefault { get; set; }

    /// <summary>
    /// Идентификатор разрешающего механика
    /// </summary>
    public Guid? PermittedMechanicId { get; set; }

    /// <summary>
    /// Разрешающий механик
    /// </summary>
    public virtual Employee? PermittedMechanic { get; set; }

    /// <summary>
    /// Идентификатор принимающего механика
    /// </summary>
    public Guid? ReceivedMechanicId { get; set; }

    /// <summary>
    /// принимающий механик
    /// </summary>
    public virtual Employee? ReceivedMechanic { get; set; }

    /// <summary>
    /// Исходящий ТС (транспортное средство) исправен или нет
    /// </summary>
    public bool IsVehicleOk { get; set; }

    /// <summary>
    /// Обратно ТС (транспортное средство) исправен или нет
    /// </summary>
    public bool IsReturnVehicleOk { get; set; }

    /// <summary>
    /// Показание спидометра в получении ТС
    /// </summary>
    public double SpeedometerIndication { get; set; }

    /// <summary>
    /// Показание спидометра в возврате ТС
    /// </summary>
    public double ReturnSpeedometer { get; set; }

    /// <summary>
    /// Идентификатор путевого листа
    /// </summary>
    public Guid WaybillId { get; set; }

    /// <summary>
    /// Путевой лист
    /// </summary>
    public virtual Waybill? Waybill { get; set; }

    /// <summary>
    /// Резервное дежурное время
    /// </summary>
    public TimeSpan? КeserveDutyTime { get; set; }

    /// <summary>
    /// Неоправданное время
    /// </summary>
    public TimeSpan? UnjustifiedTime { get; set; }

    /// <summary>
    /// Время простоя на линии
    /// </summary>
    public TimeSpan? IdleTime { get; set; }

    /// <summary>
    /// Ночное время (праздничное)
    /// </summary>
    public TimeSpan? NightOrHolidayTime { get; set; }

    /// <summary>
    /// Рейсы по плану
    /// </summary>
    public int? ScheduledRoutesCount { get; set; }

    /// <summary>
    /// Рейсы по факту
    /// </summary>
    public int? ActuallyRoutesCount { get; set; }

    public WaybillDetailStatus Status { get; set; }

    public ICollection<WaybillMechanicConclusion> MechanicConclusions { get; set; }
    public ICollection<WaybillDoctorConclusion> WaybillDoctorConclusions { get; set; }
    public ICollection<WaybillFuel> WaybillFuels { get; set; }
}
