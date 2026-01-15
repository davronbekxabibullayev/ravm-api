namespace Ravm.Domain.Models;

using Ravm.Domain.Common;

/// <summary>
/// Страна
/// </summary>
public class Country : LocalizableEntity, IDeletable
{
    /// <summary>
    /// Код страны
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Код СОАТО
    /// </summary>
    public string? StateCode { get; set; }

    public bool IsDeleted { get; set; }
}
