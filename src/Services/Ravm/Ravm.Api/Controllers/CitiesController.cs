namespace Ravm.Api.Controllers;

using Ravm.Api.Models.Cities;
using Ravm.Application.UseCases.Cities.Commands;
using Ravm.Application.UseCases.Cities.Models;
using Ravm.Application.UseCases.Cities.Queries;
using Ravm.Application.UseCases.Countries.Commands;

[Route("api/cities")]
[ApiController]
[Authorize]
public class CitiesController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Получить список городов
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<CityModel>>> GetCities([FromQuery] GetCitiesQuery query)
    {
        return await sender.Send(query);
    }

    /// <summary>
    /// Получить определенный городов по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<CityModel>> GetCity([FromRoute] Guid id)
    {
        return await sender.Send(new GetCityQuery(id));
    }

    /// <summary>
    /// Создать новый город
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateCity([FromBody] CreateCityCommand cmd)
    {
        await sender.Send(cmd);

        return Ok();
    }

    /// <summary>
    /// Обновит город по идентификатору
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCity([FromRoute] Guid id, [FromBody] UpdateCityRequest request)
    {
        await sender.Send(new UpdateCityCommand(
            id,
            request.RegionId,
            request.Name,
            request.NameRu,
            request.NameUz,
            request.NameKa,
            request.Code,
            request.StateCode));

        return Ok();
    }

    /// <summary>
    /// Удалить город по идентификатору
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCity([FromRoute] Guid id)
    {
        await sender.Send(new DeleteCityCommand(id));

        return Ok();
    }
}
