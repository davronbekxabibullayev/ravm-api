namespace Ravm.Api.Models.Waybills;

using System.IO;
using Ravm.Api.Utils.OpenXml;

public class WaybillCertificateModel : IWordpoccessingWorkerModel
{
    /// <summary>
    /// Наименомиравание организация
    /// </summary>
    public required string OrganizationName { get; set; }

    /// <summary>
    /// номер путевой листа
    /// </summary>
    public required string Number { get; set; }

    /// <summary>
    /// дата
    /// </summary>
    public string? Date { get; set; }

    /// <summary>
    /// дата
    /// </summary>
    public required string DateMonth { get; set; }

    /// <summary>
    /// дата
    /// </summary>
    public required string DateYear { get; set; }

    /// <summary>
    /// Драйвер ФИО
    /// </summary>
    public required string DriverFullName { get; set; }

    /// <summary>
    /// штатный номер водителя
    /// </summary>
    public required string StaffNumber { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public required string DriverLisenceNumber { get; set; }

    /// <summary>
    /// Наименомиравание транспорт
    /// </summary>
    public required string VehicleName { get; set; }

    /// <summary>
    /// Государственный номер автомобиля
    /// </summary>
    public required string StateNumber { get; set; }

    /// <summary>
    /// Наименомиравание гараж
    /// </summary>
    public required string GarageNumber { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public required string DispatcherFullName { get; set; }

    public ICollection<WaybillDetailCertificateModel> WaybillDetails { get; set; } = Array.Empty<WaybillDetailCertificateModel>();

    public Stream AsWordStream(string sourcePath, string qrCodePath)
    {
        var worker = new WordprocessingWorker(sourcePath);

        worker.AddPictureProperty("CertificateQRCode", qrCodePath);
        worker.AddPropertyKeyValue("OrganizationName", OrganizationName);
        worker.AddPropertyKeyValue("WaybillNumber", Number);
        worker.AddPropertyKeyValue("WaybillMonth", DateMonth);
        worker.AddPropertyKeyValue("WaybillYear", DateYear);
        worker.AddPropertyKeyValue("DriverFullName", DriverFullName);
        worker.AddPropertyKeyValue("StaffNumber", StaffNumber);
        worker.AddPropertyKeyValue("DriverLicenseNumber", DriverLisenceNumber);
        worker.AddPropertyKeyValue("VehicleMark", VehicleName);
        worker.AddPropertyKeyValue("StateNumber", StateNumber);
        worker.AddPropertyKeyValue("GarageNumber", GarageNumber);
        worker.AddPropertyKeyValue("DispatcherFullName", DispatcherFullName);

        worker.FillTable(WaybillDetails, "waybill_details_bookmark");

        worker.Save();
        worker.Close();
        return worker.GetStream();
    }
}

public class WaybillDetailCertificateModel
{
    public required string DayOfMonth { get; set; }

    /// <summary>
    /// Наименомиравание гараж
    /// </summary>
    public required string GarageNumber { get; set; }

    /// <summary>
    /// Государственный номер автомобиля
    /// </summary>
    public required string StateNumber { get; set; }

    /// <summary>
    /// Фактически время выезда
    /// </summary>
    public required string FactExitTime { get; set; }

    /// <summary>
    /// По расписанию время выезда
    /// </summary>
    public required string PlanExitTime { get; set; }

    /// <summary>
    /// Номер маршрута
    /// </summary>
    public required string RouteNumber { get; set; }

    /// <summary>
    /// Наименование  маршрута  или заказчик
    /// </summary>
    public required string RouteOrCustomerName { get; set; }

    /// <summary>
    /// Здор ли водитель
    /// </summary>
    public required string IsDriverHealthy { get; set; }

    /// <summary>
    /// Подпись механик 
    /// </summary>
    public required string SignaturePermittedMechanic { get; set; }

    /// <summary>
    /// Подпись водителя
    /// </summary>
    public required string SignatureReceiverDriver { get; set; }

    /// <summary>
    /// Выход показания спидометра
    /// </summary>
    public required string ExitSpmIndication { get; set; }

    /// <summary>
    /// Вход показания спидометра
    /// </summary>
    public string? EntrySpmIndication { get; set; }

    /// <summary>
    /// Готов ли автомобиль
    /// </summary>
    public required string IsVehicleOk { get; set; }

    /// <summary>
    /// Время возвращения в гараж
    /// </summary>
    public required string PlanEntryDate { get; set; }

    /// <summary>
    /// Время возвращения в гараж
    /// </summary>
    public string? FactEntryDate { get; set; }

    /// <summary>
    /// Автотранспортного средства  принял (подпись дежурново механика)
    /// </summary>
    public string? SignatureReceivedMechanic { get; set; }
}
