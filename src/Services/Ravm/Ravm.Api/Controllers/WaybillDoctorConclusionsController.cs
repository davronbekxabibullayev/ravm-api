namespace Ravm.Api.Controllers;

using Ravm.Api.Models.WaybillDoctorConclusions;
using Ravm.Application.UseCases.WaybillDoctorConclusions.Commands;
using Ravm.Application.UseCases.WaybillDoctorConclusions.Models;
using Ravm.Application.UseCases.WaybillDoctorConclusions.Queries;

[Route("api/waybill-doctor-conclusions")]
[ApiController]
[Authorize]
public class WaybillDoctorConclusionsController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Получить список путевой механик заключение
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<WaybillDoctorConclusionModel>>> GetWaybillDoctorConclusions([FromQuery] GetWaybillDoctorConclusionsQuery query)
    {
        return await sender.Send(query);
    }

    /// <summary>
    /// Получить путевой механик заключение по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<WaybillDoctorConclusionModel>> GetWaybillDoctorConclusion([FromRoute] Guid id)
    {
        return await sender.Send(new GetWaybillDoctorConclusionQuery(id));
    }

    /// <summary>
    /// Создать новую путевой механик заключение
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateWaybillDoctorConclusion([FromBody] CreateWaybillDoctorConclusionCommand command)
    {
        await sender.Send(command);

        return Ok();
    }

    /// <summary>
    /// Обновит путевой механик заключение
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWaybillDoctorConclusion([FromRoute] Guid id, [FromBody] UpdateWaybillDoctorConclusionRequest request)
    {
        await sender.Send(new UpdateWaybillDoctorConclusionCommand
            (id,
            request.WaybillDetailId,
            request.WaybillDriverId,
            request.Pressure,
            request.Pulse,
            request.Temperature,
            request.Note,
            request.Permitted));

        return Ok();
    }

    /// <summary>
    /// Удалить путевой механик заключение по идентификатору
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWaybillDoctorConclusion([FromRoute] Guid id)
    {
        await sender.Send(new DeleteWaybillDoctorConclusionCommand(id));

        return Ok();
    }
}
