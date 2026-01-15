namespace Ravm.Api.Services;

using System.Globalization;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using Ravm.Api.Configuration;
using Ravm.Api.Models.Waybills;
using Ravm.Application.Common;
using Ravm.Domain.Exceptions;
using SixLabors.ImageSharp;

public class WaybillCertificateGenerator(IAppDbContext appDbContext, IConfiguration configuration) : IWaybillCertificateGenerator
{
    public async Task<string> GenerateWaybillAsync(Guid waybillId)
    {
        try
        {
            var waybill = await appDbContext.Waybills
                .Include(x => x.WaybillDetails)
                 .ThenInclude(x => x.PermittedMechanic)
                .Include(x => x.WaybillDetails)
                 .ThenInclude(x => x.ReceivedMechanic)
                .Include(x => x.Organization)
                .Include(x => x.WaybillDrivers)
                 .ThenInclude(x => x.Employee)
                .Include(x => x.WaybillDetails)
                 .ThenInclude(x => x.Dispatcher)
                .Include(x => x.WaybillDetails)
                 .ThenInclude(x => x.ReceivedDriver)
                .Include(x => x.Vehicle!.VehicleModel!.VehicleMark)
                .FirstOrDefaultAsync(x => x.Id == waybillId)
                ?? throw new NotFoundException(nameof(Waybill), waybillId);

            if (!Directory.Exists(LocalConfiguration.QrCodes))
            {
                Directory.CreateDirectory(LocalConfiguration.QrCodes);
            }

            var imagePath = Path.Combine(LocalConfiguration.QrCodes, $"waybill_{waybillId}.jpg");


            if (File.Exists(imagePath))
                File.Delete(imagePath);

            using (var streamImage = new FileStream(imagePath, FileMode.Append, FileAccess.Write))
            {
                using (var qrGenerator = new QRCodeGenerator())
                using (var qrCodeData = qrGenerator.CreateQrCode($"{configuration["BackendUrl"]}/api/waybills/waybill-certificate/{waybillId}", QRCodeGenerator.ECCLevel.Q))

                using (var qrCode = new QRCode(qrCodeData))
                {
                    var image = qrCode.GetGraphic(20);

                    image.SaveAsJpeg(streamImage);
                }
            }

            Employee? driver = null;
            Employee? dispatcher = null;
            List<WaybillDetailCertificateModel>? details = null;
            var no = "нет";
            var yes = "да";
            if (waybill.WaybillDetails.Count > 0)
            {
                driver = waybill.WaybillDetails.First().ReceivedDriver;
                dispatcher = waybill.WaybillDetails.First().Dispatcher;
                details = waybill.WaybillDetails
                   .OrderBy(x => x.Date)
                   .Select(x => new WaybillDetailCertificateModel
                   {
                       SignaturePermittedMechanic = x.PermittedMechanic != null ? GetEmployeeSignature(x.PermittedMechanic!) : no,
                       SignatureReceivedMechanic = x.ReceivedMechanic != null ? GetEmployeeSignature(x.ReceivedMechanic!) : no,
                       SignatureReceiverDriver = x.ReceivedDriver != null ? GetEmployeeSignature(x.ReceivedDriver!) : no,
                       EntrySpmIndication = $"{x.ReturnSpeedometer}",
                       ExitSpmIndication = $"{x.SpeedometerIndication}",
                       DayOfMonth = $"{x.Date.Day}",
                       FactEntryDate = x.ActualEndTime.HasValue ? x.ActualEndTime.Value.ToString("hh:mm", new DateTimeFormatter()) : "-:-",
                       FactExitTime = x.ActualStartTime.HasValue ? x.ActualStartTime.Value.ToString("hh:mm", new DateTimeFormatter()) : "-:-",
                       StateNumber = waybill.Vehicle!.StateNumber,
                       GarageNumber = waybill.Vehicle.GarageNumber,
                       IsDriverHealthy = x.WaybillDoctorConclusions.FirstOrDefault() != null ? (x.WaybillDoctorConclusions.FirstOrDefault()!.Permitted ? yes : no) : no,
                       IsVehicleOk = x.IsVehicleOk ? yes : no,
                       PlanEntryDate = x.PlannedEndTime.ToString("hh:mm", new DateTimeFormatter()),
                       PlanExitTime = x.PlannedStartTime.ToString("hh:mm", new DateTimeFormatter()),
                       RouteNumber = waybill.Route != null ? (waybill.Route!.Number != null ? waybill.Route!.Number! : no) : no,
                       RouteOrCustomerName = waybill.Route != null ? waybill.Route!.Name : no
                   })
                   .ToList();
            }

            var model = new WaybillCertificateModel
            {
                OrganizationName = waybill.Organization!.NameRu,
                Number = waybill.Number,
                DateMonth = GetMonth(waybill.BeginDate.Date.Month),
                DateYear = string.Create(CultureInfo.InvariantCulture, $"{waybill.BeginDate.Year}"),
                VehicleName = waybill.Vehicle!.VehicleModel!.VehicleMark!.NameRu,
                DriverFullName = driver != null ? driver.FullName : no,
                StaffNumber = driver != null ? driver.StaffNumber : no,
                DriverLisenceNumber = driver != null ? (driver.DriverLisenceNumber != null ? driver.DriverLisenceNumber! : no) : no,
                StateNumber = waybill.Vehicle!.StateNumber,
                GarageNumber = waybill.Vehicle!.GarageNumber,
                DispatcherFullName = dispatcher != null ? dispatcher.FullName : no,
            };

            if (waybill.WaybillDetails.Count > 0)
            {
                model.WaybillDetails = details!;
            }

            var stream = model.AsWordStream(Path.Combine(LocalConfiguration.Templates, "waybill_certificate.docx"), imagePath);

/*            var fontFolderPath = LocalConfiguration.Fonts;
            var files = Directory.GetFiles(fontFolderPath);
            var fonts = new List<Stream>();
            foreach (var fontPath in files)
            {
                var fileStream = new FileStream(fontPath, FileMode.Open, FileAccess.Read);
                fonts.Add(fileStream);
            }

            var document = new Document();
            document.LoadFromStream(stream, FileFormat.Docx);
            document.SetCustomFonts([.. fonts]);

            var pdfStream = new MemoryStream();
            document.SaveToStream(pdfStream, FileFormat.PDF);
            fonts.ForEach(x => x.Close());

            pdfStream.Position = 0;*/

            if (!Directory.Exists(LocalConfiguration.Certificates))
            {
                Directory.CreateDirectory(LocalConfiguration.Certificates);
            }

            var wordPath = Path.Combine(LocalConfiguration.Certificates, $"waybill_{waybillId}.docx");
            if (File.Exists(wordPath))
            {
                File.Delete(wordPath);
            }

            using (var fs = new FileStream(wordPath, FileMode.CreateNew, FileAccess.Write))
            {
                /*var bytes = pdfStream.ToArray();
                await fs.WriteAsync(bytes, 0, bytes.Length);
                await fs.FlushAsync();*/
                await stream.CopyToAsync(fs);
                stream.Close();
            }

            return wordPath;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static string GetMonth(int month)
    {
        return month switch
        {
            1 => "январь",
            2 => "Февраль",
            3 => "март",
            4 => "апрель",
            5 => "май",
            6 => "июнь",
            7 => "июль",
            8 => "август",
            9 => "сентябрь",
            10 => "октябрь",
            11 => "ноябрь",
            12 => "декабрь",
            _ => string.Empty
        };
    }

    private static string GetEmployeeSignature(Employee employee)
    {
        return $"{employee.FirstName[0]}.{employee.LastName[0]}";
    }

    public class DateTimeFormatter : IFormatProvider
    {
        public object? GetFormat(Type? formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }
            else
            {
                return null;
            }
        }
    }
}
