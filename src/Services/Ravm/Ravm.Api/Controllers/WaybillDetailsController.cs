namespace Ravm.Api.Controllers;

using Ravm.Api.Models.Reports;
using Ravm.Api.Models.WaybillDetails;
using Ravm.Application.UseCases.WaybillDetails.Commands;
using Ravm.Application.UseCases.WaybillDetails.Models;
using Ravm.Application.UseCases.WaybillDetails.Queries;
using Ravm.Domain.Enums;

[Route("api/waybill-details")]
[ApiController]
[Authorize]
public class WaybillDetailsController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Получить список детали путевого листа
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<WaybillDetailModel>>> GetWaybillDetails([FromQuery] GetWaybillDetailsQuery query)
    {
        return await sender.Send(query);
    }

    /// <summary>
    /// Получить детали путевого листа по периодам
    /// </summary>
    [HttpGet("periods")]
    public async Task<ActionResult<PagedList<WaybillDetailModel>>> GetWaybillDetailsByPeriod([FromQuery] FilteringRequest query, [FromQuery] GetReportsByPeriodRequest request)
    {
        return await sender.Send(new GetWaybillDetailsByPeriodQuery(query)
        {
            From = request.From,
            To = request.To
        });
    }

    /// <summary>
    /// Получить детали путевого листа по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<WaybillDetailModel>> GetWaybillDetail([FromRoute] Guid id)
    {
        return await sender.Send(new GetWaybillDetailQuery(id));
    }

    /// <summary>
    /// Создать новую детали путевого листа
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateWaybillDetail([FromBody] CreateWaybillDetailCommand command)
    {
        await sender.Send(command);

        return Ok();
    }

    /// <summary>
    /// Обновит детали путевого листа
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWaybillDetail([FromRoute] Guid id, [FromBody] UpdateWaybillDetailRequest request)
    {
        await sender.Send(new UpdateWaybillDetailCommand()
        {
            Id = id,
            WaybillTaskId = request.WaybillTaskId,
            IdleReasonId = request.IdleReasonId,
            DispatcherId = request.DispatcherId,
            ManagerId = request.ManagerId,
            Date = request.Date,
            PlannedStartTime = request.PlannedStartTime,
            PlannedEndTime = request.PlannedEndTime,
            IsDefault = request.IsDefault,
            WaybillId = request.WaybillId,
            КeserveDutyTime = request.КeserveDutyTime,
            UnjustifiedTime = request.UnjustifiedTime,
            IdleTime = request.IdleTime,
            NightOrHolidayTime = request.NightOrHolidayTime,
            ScheduledRoutesCount = request.ScheduledRoutesCount,
            ActuallyRoutesCount = request.ActuallyRoutesCount,
        });

        return Ok();
    }

    /// <summary>
    /// Обновит статус путевого листа
    /// </summary>
    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateWaybillDetailStatus([FromRoute] Guid id, [FromBody] WaybillDetailStatus status)
    {
        await sender.Send(new UpdateWaybillDetailStatusCommand(id, status));

        return Ok();
    }

    /// <summary>
    /// Удалить детали путевого листа по идентификатору
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWaybillDetail([FromRoute] Guid id)
    {
        await sender.Send(new DeleteWaybillDetailCommand(id));

        return Ok();
    }
}
