namespace Ravm.Api.Controllers;

using Ravm.Api.Models.WaybillTasks;
using Ravm.Application.UseCases.WaybillTasks.Commands;
using Ravm.Application.UseCases.WaybillTasks.Models;
using Ravm.Application.UseCases.WaybillTasks.Queries;

[Route("api/waybill-tasks")]
[ApiController]
[Authorize]
public class WaybillTasksController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Получить список задачи путевого листа
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<WaybillTaskModel>>> GetWaybillTasks([FromQuery] GetWaybillTasksQuery query)
    {
        return await sender.Send(query);
    }

    /// <summary>
    /// Получить задачи путевого листа по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<WaybillTaskModel>> GetWaybillTask([FromRoute] Guid id)
    {
        return await sender.Send(new GetWaybillTaskQuery(id));
    }

    /// <summary>
    /// Создать новую задачи путевого листа
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateWaybillTask([FromBody] CreateWaybillTaskCommand command)
    {
        await sender.Send(command);

        return Ok();
    }

    /// <summary>
    /// Обновит задачи путевого листа
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWaybillTask([FromRoute] Guid id, [FromBody] UpdateWaybillTaskRequest request)
    {
        await sender.Send(new UpdateWaybillTaskCommand(
            id,
            request.Number,
            request.Customer,
            request.CargoInfo,
            request.Note,
            request.TripsAmount,
            request.Date,
            request.StartTime,
            request.EndTime,
            request.Distance,
            request.AddressTo,
            request.AddressFrom,
            request.WaybillId
            ));

        return Ok();
    }

    /// <summary>
    /// Удалить задачи путевого листа по идентификатору
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWaybillTask([FromRoute] Guid id)
    {
        await sender.Send(new DeleteWaybillTaskCommand(id));

        return Ok();
    }
}
