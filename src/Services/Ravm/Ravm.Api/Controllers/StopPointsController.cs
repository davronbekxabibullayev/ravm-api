namespace Ravm.Api.Controllers;

using Ravm.Api.Models.StopPoints;
using Ravm.Application.UseCases.StopPoints.Commands;
using Ravm.Application.UseCases.StopPoints.Models;
using Ravm.Application.UseCases.StopPoints.Queries;

[Route("api/stop-points")]
[ApiController]
[Authorize]
public class StopPointsController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    /// <summary>
    /// Получить список oстановки маршрута
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<StopPointModel>>> GetStopPoints([FromQuery] GetStopPointsQuery request)
    {
        return await _sender.Send(request);
    }

    /// <summary>
    /// Получить oстановку маршрута по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<StopPointModel>> GetStopPoint([FromRoute] Guid id)
    {
        return await _sender.Send(new GetStopPointQuery(id));
    }

    /// <summary>
    /// Создать новую oстановку маршрута
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateStopPoint([FromBody] CreateStopPointCommand request)
    {
        await _sender.Send(request);

        return Ok();
    }

    /// <summary>
    /// Обновит oстановку маршрута по идентификатору
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStopPoint([FromRoute] Guid id, [FromBody] UpdateStopPointRequest request)
    {
        await _sender.Send(new UpdateStopPointCommand(
            id,
            request.Name,
            request.NameRu,
            request.NameUz,
            request.NameKa,
            request.Code,
            request.Position));

        return Ok();
    }

    /// <summary>
    /// Удалить oстановку маршрута по идентификатору
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStopPoint([FromRoute] Guid id)
    {
        await _sender.Send(new DeleteStopPointCommand(id));

        return Ok();
    }
}
