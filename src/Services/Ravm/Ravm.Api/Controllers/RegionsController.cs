namespace Ravm.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Ravm.Api.Models.Regions;
using Ravm.Application.UseCases.Regions.Commands;
using Ravm.Application.UseCases.Regions.Models;
using Ravm.Application.UseCases.Regions.Queries;

[Route("api/regions")]
[ApiController]
[Authorize]
public class RegionsController(ISender sender) : Controller
{
    private readonly ISender _sender = sender;

    /// <summary>
    /// Получить список регионов
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<RegionModel>>> GetRegions([FromQuery] GetRegionsQuery query)
    {
        return await _sender.Send(query);
    }

    /// <summary>
    /// Получить регион по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<RegionModel>> GetRegion(Guid id)
    {
        return await _sender.Send(new GetRegionQuery(id));
    }

    /// <summary>
    /// Создать новый регион
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateRegion([FromBody] CreateRegionCommand command)
    {
        await _sender.Send(command);

        return Ok();
    }

    /// <summary>
    /// Обновит регион по идентификатору
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequest request)
    {
        await _sender.Send(new UpdateRegionCommand(
            id,
            request.Name,
            request.NameRu,
            request.NameUz,
            request.NameKa,
            request.CountryId,
            request.Code,
            request.StateCode));

        return Ok();
    }

    /// <summary>
    /// Удалить регион по идентификатору
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRegion(Guid id)
    {
        await _sender.Send(new DeleteRegionCommand(id));

        return Ok();
    }
}
