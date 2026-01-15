namespace Ravm.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Ravm.Api.Models.Occupation;
using Ravm.Application.UseCases.Occupations.Commands;
using Ravm.Application.UseCases.Occupations.Models;
using Ravm.Application.UseCases.Occupations.Queries;

[Route("api/occupations")]
[ApiController]
[Authorize]
public class OccupationsController(ISender sender) : Controller
{
    private readonly ISender _sender = sender;

    /// <summary>
    /// Получить коллекцию должностей
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<OccupationModel>>> GetOccupations([FromQuery] GetOccupationsQuery query)
    {
        return await _sender.Send(query);
    }

    /// <summary>
    /// Получить должность по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<OccupationModel>> GetOccupation(Guid id)
    {
        return await _sender.Send(new GetOccupationQuery(id));
    }

    /// <summary>
    /// Создать должность
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateOccupation([FromBody] CreateOccupationCommand command)
    {
        await _sender.Send(command);

        return Ok();
    }

    /// <summary>
    /// Обновить должность
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOccupation([FromRoute] Guid id, [FromBody] UpdateOccupationRequest request)
    {
        await _sender.Send(new UpdateOccupationCommand(
            id,
            request.Name,
            request.NameRu,
            request.NameUz,
            request.NameKa,
            request.Code));

        return Ok();
    }

    /// <summary>
    /// Удалить должность по идентификатору
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOccupation(Guid id)
    {
        await _sender.Send(new DeleteOccupationCommand(id));

        return Ok();
    }
}
