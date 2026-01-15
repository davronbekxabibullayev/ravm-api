namespace Ravm.Domain.Models;

using Ravm.Domain.Common;

/// <summary>
/// Модель ТС
/// </summary>
public class VehicleModel : LocalizableEntity, IDeletable
{
    /// <summary>
    /// Идентификатор марки ТС
    /// </summary>
    public Guid VehicleMarkId { get; set; }

    /// <summary>
    /// Марка ТС
    /// </summary>
    public virtual VehicleMark? VehicleMark { get; set; }

    /// <summary>
    /// Код модели
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Удален или нет
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Норма расхода топлива
    /// </summary>
    public double? FuelRate { get; set; }

    /// <summary>
    /// Норма расхода топливо с прицепом
    /// </summary>
    public double? FuelRateWithTrailer { get; set; }

    /// <summary>
    /// Норма расхода топливо в загруженном езде
    /// </summary>
    public double? FuelRateLoaded { get; set; }

    /// <summary>
    /// Норма расхода топливо за час работы мотора
    /// </summary>
    public double? FuelRateEngineOperation { get; set; }

    /// <summary>
    /// Норма расхода топливо за час работы мотора в загружонном состоянии
    /// </summary>
    public double? FuelRateLoadedEngineOperation { get; set; }
}
