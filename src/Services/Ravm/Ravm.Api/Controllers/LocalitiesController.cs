namespace Ravm.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Ravm.Api.Models.Localities;
using Ravm.Application.UseCases.Localities.Commands;
using Ravm.Application.UseCases.Localities.Models;
using Ravm.Application.UseCases.Localities.Queries;

[Route("api/localities")]
[ApiController]
[Authorize]
public class LocalitiesController(ISender sender) : Controller
{
    private readonly ISender _sender = sender;

    /// <summary>
    ///  Получить населенный пункт по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<LocalityModel>> GetLocality(Guid id)
    {
        return await _sender.Send(new GetLocalityQuery(id));
    }

    /// <summary>
    ///  Получить список населенных пунктов
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<LocalityModel>>> GetLocalities([FromQuery] GetLocalitiesQuery request)
    {
        return await _sender.Send(request);
    }

    /// <summary>
    /// Создать новый населенный пункт
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateLocality([FromBody] CreateLocalityCommand command)
    {
        await _sender.Send(command);
        return Ok();
    }
    /// <summary>
    /// Обновить населенный пункт по идентификатору
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLocality([FromRoute] Guid id, [FromBody] UpdateLocalityRequest request)
    {
        await _sender.Send(new UpdateLocalityCommand(
            id,
            request.Name,
            request.NameRu,
            request.NameKa,
            request.NameUz,
            request.Code,
            request.RegionId,
            request.CityId,
            request.StateCode));

        return Ok();
    }

    /// <summary>
    /// Удалить населенный пункт по идентификатору
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLocality(Guid id)
    {
        await _sender.Send(new DeleteLocalityCommand(id));

        return Ok();
    }
}
