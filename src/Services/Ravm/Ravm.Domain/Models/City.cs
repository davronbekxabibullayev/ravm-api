namespace Ravm.Domain.Models;

using Ravm.Domain.Common;

/// <summary>
/// Город (город/район)
/// </summary>
public class City : LocalizableEntity, IDeletable
{
    /// <summary>
    /// Идентификатор региона 
    /// </summary>
    public Guid RegionId { get; set; }

    /// <summary>
    /// Регион
    /// </summary>
    public virtual Region? Region { get; set; }

    /// <summary>
    /// Код города
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Код СОАТО
    /// </summary>
    public string? StateCode { get; set; }

    public bool IsDeleted { get; set; }
}
