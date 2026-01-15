namespace Ravm.Domain.Models;

using Ravm.Domain.Common;

/// <summary>
/// Задачи путевого листа
/// </summary>
public class WaybillTask : Entity, IDeletable
{
    /// <summary>
    /// Номер задачи путевого листа
    /// </summary>
    public required string Number { get; set; }

    /// <summary>
    /// Удален или нет
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Заказчик
    /// </summary>
    public string? Customer { get; set; }

    /// <summary>
    /// Информация о грузе
    /// </summary>
    public string? CargoInfo { get; set; }

    /// <summary>
    /// Примечание
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Количество проездов
    /// </summary>
    public int TripsAmount { get; set; }

    /// <summary>
    /// Дата задачи
    /// </summary>
    public DateTimeOffset Date { get; set; }

    /// <summary>
    /// Время начало
    /// </summary>
    public TimeSpan StartTime { get; set; }

    /// <summary>
    /// Время завершение
    /// </summary>
    public TimeSpan EndTime { get; set; }

    /// <summary>
    /// Пройденное дистанция
    /// </summary>
    public double Distance { get; set; }

    /// <summary>
    /// Адрес с
    /// </summary>
    public string? AddressTo { get; set; }

    /// <summary>
    /// Адрес по
    /// </summary>
    public string? AddressFrom { get; set; }

    /// <summary>
    /// Идентификатор путевого листа
    /// </summary>
    public Guid WaybillId { get; set; }

    /// <summary>
    /// Путевой лист
    /// </summary>
    public virtual Waybill? Waybill { get; set; }
}
