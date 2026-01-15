namespace Ravm.Domain.Models;

using Ravm.Domain.Common;

/// <summary>
/// Должност
/// </summary>
public class Occupation : LocalizableEntity, IDeletable
{
    /// <summary>
    /// Код должности
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Удален или нет
    /// </summary>
    public bool IsDeleted { get; set; }
}
