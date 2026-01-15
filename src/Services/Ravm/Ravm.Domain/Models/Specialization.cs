namespace Ravm.Domain.Models;

using Ravm.Domain.Common;

/// <summary>
/// Специализация
/// </summary>
public class Specialization : LocalizableEntity, IDeletable
{
    /// <summary>
    /// Код специализации
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Удален или нет
    /// </summary>
    public bool IsDeleted { get; set; }
}
