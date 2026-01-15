namespace Ravm.Application.UseCases.Reports.Drivers.Models;

public class ReportDriverDetailDatas
{
    /// <summary>
    /// Гаражный номер ТС
    /// </summary>
    public required string VehicleGarageNumber { get; set; }

    /// <summary>
    /// Марка ТС
    /// </summary>
    public required string VehicleMark { get; set; }

    /// <summary>
    /// Государственный номер ТС
    /// </summary>
    public required string VehicleStateNumbers { get; set; }

    /// <summary>
    /// Номер маршрута
    /// </summary>
    public string? RouteNumber { get; set; }

    /// <summary>
    /// Отметки о приключениях на другом маршруте
    /// </summary>
    public string? NotesAnotherRoute { get; set; }

    /// <summary>
    /// Дата 
    /// </summary>
    public DateTimeOffset Date { get; set; }

    /// <summary>
    /// Часы работы на маршруте
    /// </summary>
    public TimeSpan? WorkedHoursOnRoute { get; set; }

    /// <summary>
    /// Часы работы над задачей
    /// </summary>
    public TimeSpan? WorkedHoursOnTask { get; set; }

    /// <summary>
    /// Резервное дежурное время
    /// </summary>
    public TimeSpan? КeserveDutyTime { get; set; }

    /// <summary>
    /// Неоправданное время
    /// </summary>
    public TimeSpan? UnjustifiedTime { get; set; }

    /// <summary>
    /// Время простоя на линии
    /// </summary>
    public TimeSpan? IdleTime { get; set; }

    /// <summary>
    /// Ночное время (праздничное)
    /// </summary>
    public TimeSpan? NightOrHolidayTime { get; set; }

    /// <summary>
    /// Рейсы по плану
    /// </summary>
    public int? ScheduledRoutesCount { get; set; }

    /// <summary>
    /// Рейсы по факту
    /// </summary>
    public int? ActuallyRoutesCount { get; set; }

    /// <summary>
    /// Пройденное расстояние по заданию
    /// </summary>
    public double TraveledDistanceTask { get; set; }

    /// <summary>
    /// Пройденное расстояние маршрут
    /// </summary>
    public double TraveledDistanceRoute { get; set; }

    /// <summary>
    /// Полученное имя механика
    /// </summary>
    public required string ReceivedMechanicName { get; set; }
}
