namespace Ravm.Api.Controllers;

using Ravm.Api.Models.WaybillMechanicConclusions;
using Ravm.Application.UseCases.WaybillMechanicConclusions.Commands;
using Ravm.Application.UseCases.WaybillMechanicConclusions.Models;
using Ravm.Application.UseCases.WaybillMechanicConclusions.Queries;

[Route("api/waybill-mechanic-conclusions")]
[ApiController]
[Authorize]
public class WaybillMechanicCunclusionsController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Получить список путевой врача заключение
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<WaybillMechanicConclusionModel>>> GetWaybillMechanicConclusions([FromQuery] GetWaybillMechanicConclusionsQuery query)
    {
        return await sender.Send(query);
    }

    /// <summary>
    /// Получить путевой врача заключение по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<WaybillMechanicConclusionModel>> GetWaybillMechanicConclusion([FromRoute] Guid id)
    {
        return await sender.Send(new GetWaybillMechanicConclusionQuery(id));
    }

    /// <summary>
    /// Создать новую путевой врача заключение
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateWaybillMechanicConclusion([FromBody] CreateWaybillMechanicConclusionCommand command)
    {
        await sender.Send(command);

        return Ok();
    }

    /// <summary>
    /// Обновит путевой врача заключение
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWaybillMechanicConclusion([FromRoute] Guid id, [FromBody] UpdateWaybillMechanicConclusionRequest request)
    {
        await sender.Send(new UpdateWaybillMechanicConclusionCommand
            (id,
            request.WaybillDetailId,
            request.ReceivedDriverId,
            request.ReturnedDriverId,
            request.IsEngineHealthy,
            request.IsTireHealthy,
            request.IsBrakeHealthy,
            request.IsTransmissionHealthy,
            request.MechanicConclusionType,
            request.Note,
            request.SpeedometerIndication,
            request.ReturnSpeedometer,
            request.IsVehicleHealthy,
            request.FuelAmount));

        return Ok();
    }

    /// <summary>
    /// Удалить путевой врача заключение по идентификатору
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWaybillMechanicConclusion(Guid id)
    {
        await sender.Send(new DeleteWaybillMechanicConclusionCommand(id));

        return Ok();
    }
}
