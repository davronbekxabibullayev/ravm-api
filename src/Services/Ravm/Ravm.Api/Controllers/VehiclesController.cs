namespace Ravm.Api.Controllers;

using Ravm.Api.Models.Vehicles;
using Ravm.Application.UseCases.Vehicles.Commands;
using Ravm.Application.UseCases.Vehicles.Models;
using Ravm.Application.UseCases.Vehicles.Queries;
using Ravm.Domain.Models;

[Route("api/vehicles")]
[ApiController]
[Authorize]
public class VehiclesController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    /// <summary>
    /// Получить список транспортных средств
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<VehicleItemDto>>> GetVehicles([FromQuery] GetVehiclesQuery request)
    {
        return await _sender.Send(request);
    }

    /// <summary>
    /// Получить транспортного средства по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<VehicleItemDto>> GetVehicle([FromRoute] Guid id)
    {
        return await _sender.Send(new GetVehicleQuery(id));
    }

    /// <summary>
    /// Создать новую транспортного средства
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleCommand request)
    {
        await _sender.Send(request);

        return Ok();
    }

    /// <summary>
    /// Обновит транспортного средства по идентификатору
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVehicle([FromRoute] Guid id, [FromBody] UpdateVehicleRequest request)
    {
        await _sender.Send(new UpdateVehicleCommand(
            id,
            request.OrganizationId,
            request.VehicleModelId,
            request.StateNumber,
            request.GarageNumber,
            request.Vin,
            request.ChassisNumber));

        return Ok();
    }

    /// <summary>
    /// Удалить транспортного средства по идентификатору
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehicle([FromRoute] Guid id)
    {
        await _sender.Send(new DeleteVehicleCommand(id));

        return Ok();
    }
}
