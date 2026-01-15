namespace Ravm.Domain.Common;

/// <summary>
/// Базовый класс для сущностей поддерживающих локализацию 
/// </summary>
public abstract class LocalizableEntity : Entity, ILocalizableEntity
{
    /// <summary>
    /// Наименование на узбекском
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Наименование на русском
    /// </summary>
    public required string NameRu { get; set; }

    /// <summary>
    /// Наименование на узбекском кириллице
    /// </summary>
    public string? NameUz { get; set; }

    /// <summary>
    /// Наименование на каракалпакском
    /// </summary>
    public string? NameKa { get; set; }
}
