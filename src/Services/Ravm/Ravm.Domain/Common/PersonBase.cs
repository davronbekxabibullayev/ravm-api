namespace Ravm.Domain.Common;

using Ravm.Domain.Enums;

/// <summary>
/// Базовый класс для людей
/// </summary>
public abstract class PersonBase : Entity
{
    /// <summary>
    /// Имя
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Отчество
    /// </summary>
    public string? MiddleName { get; set; }

    /// <summary>
    /// Полное имя
    /// </summary>
    public string FullName { get; private set; } = default!;

    /// <summary>
    /// Пол
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// Дата рождение
    /// </summary>
    public DateTimeOffset? BirthDate { get; set; }

    /// <summary>
    /// День рождение 
    /// </summary>
    public int? BirthDay { get; set; }

    /// <summary>
    /// Месяц рождение
    /// </summary>
    public int? BirthMonth { get; set; }

    /// <summary>
    /// Год рождение
    /// </summary>
    public int? BirthYear { get; set; }

    /// <summary>
    /// Паспортный штрих-код
    /// </summary>
    public string? Pin { get; set; }
}
