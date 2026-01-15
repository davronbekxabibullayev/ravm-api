namespace Ravm.Domain.Models;

using Ravm.Domain.Common;
using Ravm.Domain.Enums;

/// <summary>
/// Tочка остановки
/// </summary>
public class StopPoint : LocalizableEntity, IDeletable
{
    /// <summary>
    /// Код
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Положение точки остановки
    /// </summary>
    public StopPointPosition Position { get; set; }

    /// <summary>
    /// Удален или нет
    /// </summary>
    public bool IsDeleted { get; set; }
}
