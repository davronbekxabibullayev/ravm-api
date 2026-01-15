namespace Ravm.Api.Controllers;

using Microsoft.EntityFrameworkCore;
using Ravm.Api.Configuration;
using Ravm.Api.Models.Waybills;
using Ravm.Api.Services;
using Ravm.Application.Common;
using Ravm.Application.UseCases.Waybills.Commands;
using Ravm.Application.UseCases.Waybills.Models;
using Ravm.Application.UseCases.Waybills.Queries;
using Ravm.Domain.Exceptions;

[Route("api/waybills")]
[ApiController]
[Authorize]
public class WaybillsController(ISender sender, IAppDbContext dbContext, IWaybillCertificateGenerator certificateGenerator) : ControllerBase
{
    /// <summary>
    /// Получить список путевой лист
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<WaybillModel>>> GetWaybills([FromQuery] GetWaybillsQuery query)
    {
        return await sender.Send(query);
    }

    /// <summary>
    /// Получить путевой лист по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<WaybillModel> GetWaybill([FromRoute] Guid id)
    {
        return await sender.Send(new GetWaybillQuery(id));
    }

    /// <summary>
    /// Создать новую путевой лист
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateWaybill([FromBody] CreateWaybillCommand command)
    {
        await sender.Send(command);

        return Ok();
    }

    /// <summary>
    /// Обновит путевой лист
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWaybill([FromRoute] Guid id, [FromBody] UpdateWaybillRequest request)
    {
        await sender.Send(new UpdateWaybillCommand
            (
            id,
            request.Number,
            request.OrganizationId,
            request.ExpireDate,
            request.BeginDate,
            request.RouteId,
            request.VehicleId,
            request.DriverIds));

        return Ok();
    }

    /// <summary>
    /// Удалить путевой лист по идентификатору
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWaybill([FromRoute] Guid id)
    {
        await sender.Send(new DeleteWaybillCommand(id));

        return Ok();
    }

    /// <summary>
    /// Создать PDF-файл для путевой лист
    /// </summary>
    [HttpPost("{id}/generate-pdf")]
    public async Task<ActionResult<string>> GeneratePdf(Guid id)
    {
        var response = await certificateGenerator.GenerateWaybillAsync(id);

        return Ok(response);
    }

    /// <summary>
    /// Cкачать сертификат путевой лист
    /// </summary>
    [HttpGet("{id}/certificate")]
    public async Task<IActionResult> DownloadCertificate(Guid id)
    {
        var waybill = await dbContext.Waybills.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException(nameof(Waybill), id);

        var wordPath = Path.Combine(LocalConfiguration.Certificates, $"waybill_{id}.docx");

        return File(System.IO.File.OpenRead(wordPath), "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
    }

    /// <summary>
    /// Cкачать QR код путевой лист
    /// </summary>
    [HttpGet("{id}/qr-code")]
    public async Task<IActionResult> DownloadQrCode(Guid id)
    {
        var waybill = await dbContext.Waybills.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException(nameof(Waybill), id);

        var pdfPath = Path.Combine(LocalConfiguration.QrCodes, $"waybill_{id}.jpg");

        return File(System.IO.File.OpenRead(pdfPath), "image/jpg");
    }
}
