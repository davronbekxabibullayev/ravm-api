namespace Ravm.Domain.Models;

using Ravm.Domain.Common;

/// <summary>
/// Марка ТС
/// </summary>
public class VehicleMark : LocalizableEntity, IDeletable
{
    /// <summary>
    /// Код марки
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Удален или нет
    /// </summary>
    public bool IsDeleted { get; set; }
}
