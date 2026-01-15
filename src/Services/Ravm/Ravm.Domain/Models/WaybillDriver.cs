namespace Ravm.Domain.Models;

using Ravm.Domain.Common;
using Ravm.Domain.Enums;

/// <summary>
/// Водители путевого листа
/// </summary>
public class WaybillDriver : Entity
{
    /// <summary>
    /// Идентификатор сотрудника (водителья)
    /// </summary>
    public Guid EmployeeId { get; set; }

    /// <summary>
    /// Идентификатор путевого листа
    /// </summary>
    public Guid WaybillId { get; set; }

    /// <summary>
    /// Сотрудник (водитель)
    /// </summary>
    public virtual Employee? Employee { get; set; }

    /// <summary>
    /// Путевой лист
    /// </summary>
    public virtual Waybill? Waybill { get; set; }

    /// <summary>
    /// Роль водителя-путевого листа
    /// </summary>
    public WaybillDriverRole? WaybillDriverRole { get; set; }
}
