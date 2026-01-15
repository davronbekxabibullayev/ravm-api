namespace Ravm.Domain.Models;

using Ravm.Domain.Common;

/// <summary>
/// Населенные пункты
/// </summary>
public class Locality : LocalizableEntity, IDeletable
{
    /// <summary>
    /// Идентификатор города
    /// </summary>
    public Guid? CityId { get; set; }

    /// <summary>
    /// Город
    /// </summary>
    public virtual City? City { get; set; }

    /// <summary>
    /// Идентификатор региона
    /// </summary>
    public Guid? RegionId { get; set; }

    /// <summary>
    /// Регилн
    /// </summary>
    public virtual Region? Region { get; set; }

    /// <summary>
    /// Код населенного пункта
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Код СОАТО
    /// </summary>
    public string? StateCode { get; set; }

    public bool IsDeleted { get; set; }
}
