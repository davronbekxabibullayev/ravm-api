namespace Ravm.Api.Controllers;

using Ravm.Api.Models.VehicleMarks;
using Ravm.Application.UseCases.VehicleMarks.Commands;
using Ravm.Application.UseCases.VehicleMarks.Models;
using Ravm.Application.UseCases.VehicleMarks.Queries;

[Route("api/vehicle-marks")]
[ApiController]
[Authorize]
public class VehicleMarksController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    /// <summary>
    /// Получить список марки транспортных средств
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<VehicleMarkModel>>> GetVehicleMarks([FromQuery] GetVehicleMarksQuery request)
    {
        return await _sender.Send(request);
    }

    /// <summary>
    /// Получить марка транспортного средства по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<VehicleMarkModel>> GetVehicleMark([FromRoute] Guid id)
    {
        return await _sender.Send(new GetVehicleMarkQuery(id));
    }

    /// <summary>
    /// Создать новую марка транспортного средства
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateVehicleMark([FromBody] CreateVehicleMarkCommand request)
    {
        await _sender.Send(request);

        return Ok();
    }

    /// <summary>
    /// Обновит марка транспортного средства по идентификатору
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVehicleMark([FromRoute] Guid id, [FromBody] UpdateVehicleMarkRequest request)
    {
        await _sender.Send(new UpdateVehicleMarkCommand(
            id,
            request.Name,
            request.NameRu,
            request.NameUz,
            request.NameKa,
            request.Code));

        return Ok();
    }

    /// <summary>
    /// Удалить марка транспортного средства по идентификатору
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehicleMark([FromRoute] Guid id)
    {
        await _sender.Send(new DeleteVehicleMarkCommand(id));

        return Ok();
    }
}
