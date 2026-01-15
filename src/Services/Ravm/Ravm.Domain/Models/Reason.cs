namespace Ravm.Domain.Models;

using Ravm.Domain.Common;

/// <summary>
/// Причина
/// </summary>
public class Reason : LocalizableEntity, IDeletable
{
    /// <summary>
    /// Код причины
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Удален или нет
    /// </summary>
    public bool IsDeleted { get; set; }
}
