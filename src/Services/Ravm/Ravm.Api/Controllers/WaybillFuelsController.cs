namespace Ravm.Api.Controllers;

using Ravm.Api.Models.WaybillFuels;
using Ravm.Application.UseCases.WaybillFuels.Commands;
using Ravm.Application.UseCases.WaybillFuels.Models;
using Ravm.Application.UseCases.WaybillFuels.Queries;

[Route("api/waybill-fuels")]
[ApiController]
[Authorize]
public class WaybillFuelsController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Получить список заправка топливо по путевому листу
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<WaybillFuelModel>>> GetWaybillFuels([FromQuery] GetWaybillFuelsQuery query)
    {
        return await sender.Send(query);
    }

    /// <summary>
    /// Получить заправка топливо по путевому листу по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<WaybillFuelModel>> GetWaybillFuel([FromRoute] Guid id)
    {
        return await sender.Send(new GetWaybillFuelQuery(id));
    }

    /// <summary>
    /// Создать новую заправка топливо по путевому листу
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateWaybillFuel([FromBody] CreateWaybillFuelCommand command)
    {
        await sender.Send(command);

        return Ok();
    }

    /// <summary>
    /// Обновит заправка топливо по путевому листу
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWaybillFuel([FromRoute] Guid id, [FromBody] UpdateWaybillFuelRequest request)
    {
        await sender.Send(new UpdateWaybillFuelCommand(
            id,
            request.FundingSource,
            request.RefuellerFullName,
            request.RefuelDate,
            request.FuelMark,
            request.FuelType,
            request.Amount,
            request.Price,
            request.WaybillId,
            request.WaybillDetailId));

        return Ok();
    }

    /// <summary>
    /// Удалить заправка топливо по путевому листу по идентификатору
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWaybillFuel([FromRoute] Guid id)
    {
        await sender.Send(new DeleteWaybillFuelCommand(id));

        return Ok();
    }
}
