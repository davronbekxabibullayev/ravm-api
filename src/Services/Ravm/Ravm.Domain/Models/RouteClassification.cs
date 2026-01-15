namespace Ravm.Domain.Models;

using Ravm.Domain.Common;

/// <summary>
/// Классификация марщрута
/// </summary>
public class RouteClassification : LocalizableEntity, IDeletable
{
    /// <summary>
    /// Код классификации
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Удален или нет
    /// </summary>
    public bool IsDeleted { get; set; }
}
