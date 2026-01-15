namespace Ravm.Api.Controllers;

using Ravm.Api.Models.VehicleModels;
using Ravm.Application.UseCases.VehicleModels.Commands;
using Ravm.Application.UseCases.VehicleModels.Models;
using Ravm.Application.UseCases.VehicleModels.Queries;

[Route("api/vehicle-models")]
[ApiController]
[Authorize]
public class VehicleModelsController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    /// <summary>
    /// Получить список моделей транспортных средств
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<VehicleModelModel>>> GetVehicleModels([FromQuery] GetVehicleModelsQuery request)
    {
        return await _sender.Send(request);
    }

    /// <summary>
    /// Получить модель транспортного средства по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<VehicleModelModel>> GetVehicleModel([FromRoute] Guid id)
    {
        return await _sender.Send(new GetVehicleModelQuery(id));
    }

    /// <summary>
    /// Создать новую модель транспортного средства
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateVehicleModel([FromBody] CreateVehicleModelCommand request)
    {
        await _sender.Send(request);

        return Ok();
    }

    /// <summary>
    /// Обновит модель транспортного средства по идентификатору
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVehicleModel([FromRoute] Guid id, [FromBody] UpdateVehicleModelRequest request)
    {
        await _sender.Send(new UpdateVehicleModelCommand(
            id,
            request.Name,
            request.NameRu,
            request.NameUz,
            request.NameKa,
            request.Code,
            request.VehicleMarkId,
            request.FuelRate,
            request.FuelRateWithTrailer,
            request.FuelRateLoaded,
            request.FuelRateEngineOperation,
            request.FuelRateLoadedEngineOperation));

        return Ok();
    }

    /// <summary>
    /// Удалить модель транспортного средства по идентификатору
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehicleModel([FromRoute] Guid id)
    {
        await _sender.Send(new DeleteVehicleModelCommand(id));

        return Ok();
    }
}
