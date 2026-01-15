namespace Ravm.Api.Controllers;

using Ravm.Api.Models.RouteClassifications;
using Ravm.Application.UseCases.RouteClassifications.Commands;
using Ravm.Application.UseCases.RouteClassifications.Models;
using Ravm.Application.UseCases.RouteClassifications.Queries;

[Route("api/route-classifications")]
[ApiController]
[Authorize]
public class RouteClassificationsController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    /// <summary>
    /// Получить список классификаций маршрута
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<RouteClassificationModel>>> GetRouteClassifications([FromQuery] GetRouteClassificationsQuery request)
    {
        return await _sender.Send(request);
    }

    /// <summary>
    /// Получить классификацию маршрута по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<RouteClassificationModel>> GetRouteClassification([FromRoute] Guid id)
    {
        return await _sender.Send(new GetRouteClassificationQuery(id));
    }

    /// <summary>
    /// Создать новую классификацию маршрута
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateRouteClassification([FromBody] CreateRouteClassificationCommand request)
    {
        await _sender.Send(request);

        return Ok();
    }

    /// <summary>
    /// Обновит классификацию маршрута по идентификатору
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRouteClassification([FromRoute] Guid id, [FromBody] UpdateRouteClassificationRequest request)
    {
        await _sender.Send(new UpdateRouteClassificationCommand(
            id,
            request.Name,
            request.NameRu,
            request.NameUz,
            request.NameKa,
            request.Code));

        return Ok();
    }

    /// <summary>
    /// Удалить классификацию маршрута по идентификатору
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRouteClassification([FromRoute] Guid id)
    {
        await _sender.Send(new DeleteRouteClassificationCommand(id));

        return Ok();
    }
}
