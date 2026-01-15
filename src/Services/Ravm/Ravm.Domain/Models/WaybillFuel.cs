namespace Ravm.Domain.Models;

using Ravm.Domain.Common;
using Ravm.Domain.Enums;

/// <summary>
/// Информация о заправке
/// </summary>
public class WaybillFuel : AuditableEntity, IDeletable
{
    /// <summary>
    /// Удален или нет
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Источник финансирование
    /// </summary>
    public FundingSource FundingSource { get; set; }

    /// <summary>
    /// ФИО заправщика
    /// </summary>
    public required string RefuellerFullName { get; set; }

    /// <summary>
    /// Дата заправки
    /// </summary>
    public DateTimeOffset RefuelDate { get; set; }

    /// <summary>
    /// Марка гарючего
    /// </summary>
    public required string FuelMark { get; set; }

    /// <summary>
    /// Тип горючего
    /// </summary>
    public FuelType FuelType { get; set; }

    /// <summary>
    /// Количество горючего
    /// </summary>
    public double Amount { get; set; }

    /// <summary>
    /// Цена горючего
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Идентификатор путевого листа
    /// </summary>
    public Guid WaybillId { get; set; }

    /// <summary>
    /// Путевой лист
    /// </summary>
    public virtual Waybill? Waybill { get; set; }

    /// <summary>
    /// Идентификатор детали путевого листа
    /// </summary>
    public Guid WaybillDetailId { get; set; }

    /// <summary>
    /// Детали путевого листа
    /// </summary>
    public virtual WaybillDetail? WaybillDetail { get; set; }
}
