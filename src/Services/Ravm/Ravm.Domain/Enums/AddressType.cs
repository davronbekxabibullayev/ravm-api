namespace Ravm.Domain.Enums;

/// <summary>
/// Тип адреса
/// </summary>
public enum AddressType
{
    /// <summary>
    /// Не определен
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Адрес регистрации
    /// </summary>
    Registered = 1,

    /// <summary>
    /// Адрес проживание
    /// </summary>
    Living = 2,

    /// <summary>
    /// Почтовый адрес
    /// </summary>
    Mailing = 3,

    /// <summary>
    /// Адрес для расчетов
    /// </summary>
    Billing = 4

}
