namespace Ravm.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Ravm.Api.Models.Routes;
using Ravm.Application.UseCases.Routes.Commands;
using Ravm.Application.UseCases.Routes.Models;
using Ravm.Application.UseCases.Routes.Queries;
using Ravm.Application.UseCases.RouteStopPoints.Commands;

[Route("api/routes")]
[ApiController]
[Authorize]
public class RoutesController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Получить список маршрут
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<RouteModel>>> GetRoutes([FromQuery] GetRoutesQuery request)
    {
        return await sender.Send(request);
    }

    /// <summary>
    /// Получить маршрут по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<RouteWithDetailsModel>> GetRoute([FromRoute] Guid id)
    {
        return await sender.Send(new GetRouteQuery(id));
    }

    /// <summary>
    /// Создать новую маршрут
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateRoute([FromBody] CreateRouteCommand request)
    {
        await sender.Send(request);

        return Ok();
    }

    /// <summary>
    /// Обновит маршрут по идентификатору
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRoute([FromRoute] Guid id, [FromBody] UpdateRouteRequest request)
    {
        await sender.Send(new UpdateRouteCommand(
            id,
            request.Name,
            request.NameRu,
            request.NameUz,
            request.NameKa,
            request.Number,
            request.RouteClassificationId,
            request.Distance,
            request.TripDuration,
            request.RouteSeason,
            request.RouteOpenedDate,
            request.Note,
            request.RouteVehicleAmount,
            request.BackRouteVehicleAmount,
            request.OrganizationId
            )
        { StopPoints = request.StopPoints });

        return Ok();
    }

    /// <summary>
    /// Удалить маршрут по идентификатору
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoute([FromRoute] Guid id)
    {
        await sender.Send(new DeleteRouteCommand(id));

        return Ok();
    }

    /// <summary>
    /// Удалить точку остановки маршрута по идентификатору
    /// </summary>
    [HttpDelete("{id}/stop-points/{stopPointId}")]
    public async Task<IActionResult> DeleteRouteStopPoint([FromRoute] Guid id, [FromRoute] Guid stopPointId)
    {
        await sender.Send(new DeleteRouteStopPointCommand(id, stopPointId));

        return Ok();
    }
}
