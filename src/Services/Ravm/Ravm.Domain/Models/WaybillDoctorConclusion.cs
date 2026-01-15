namespace Ravm.Domain.Models;

using Ravm.Domain.Common;

/// <summary>
/// Заключения доктор по путевой листа
/// </summary>
public class WaybillDoctorConclusion : AuditableEntity, IDeletable
{
    /// <summary>
    /// Идентификатор врача путевой лист
    /// </summary>
    public Guid DoctorId { get; set; }

    /// <summary>
    /// Врач
    /// </summary>
    public virtual Employee? Doctor { get; set; }

    /// <summary>
    /// Идентификатор водителя путевой лист
    /// </summary>
    public Guid WaybillDriverId { get; set; }

    /// <summary>
    /// Водители путевого листа
    /// </summary>
    public virtual WaybillDriver? WaybillDriver { get; set; }

    /// <summary>
    /// Давление
    /// </summary>
    public string? Pressure { get; set; }

    /// <summary>
    /// Пульс
    /// </summary>
    public string? Pulse { get; set; }

    /// <summary>
    /// Температура
    /// </summary>
    public string? Temperature { get; set; }

    /// <summary>
    /// Примечание
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Разрешенный
    /// </summary>
    public bool Permitted { get; set; }

    /// <summary>
    /// Удален или нет
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Guid WaybillDetailId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public virtual WaybillDetail? WaybillDetail { get; set; }
}
