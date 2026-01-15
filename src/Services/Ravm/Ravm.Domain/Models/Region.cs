namespace Ravm.Domain.Models;

using Ravm.Domain.Common;

/// <summary>
/// Регион (область/города областного назначение)
/// </summary>
public class Region : LocalizableEntity, IDeletable
{
    /// <summary>
    /// Идентификатор страны
    /// </summary>
    public Guid CountryId { get; set; }

    /// <summary>
    /// Страна
    /// </summary>
    public virtual Country? Country { get; set; }

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
