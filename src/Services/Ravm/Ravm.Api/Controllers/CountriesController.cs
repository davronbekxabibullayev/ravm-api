namespace Ravm.Api.Controllers;

using Ravm.Api.Models.Countries;
using Ravm.Application.UseCases.Countries.Commands;
using Ravm.Application.UseCases.Countries.Models;
using Ravm.Application.UseCases.Countries.Queries;

[Route("api/countries")]
[ApiController]
[Authorize]
public class CountriesController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Получить список стран
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<CountryModel>>> GetCountries([FromQuery] GetCountriesQuery request)
    {
        return await sender.Send(request);
    }

    /// <summary>
    /// Получить страну по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<CountryModel>> GetCountry([FromRoute] Guid id)
    {
        return await sender.Send(new GetCountryQuery(id));
    }

    /// <summary>
    /// Создать новую страну
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateCountry([FromBody] CreateCountryCommand request)
    {
        await sender.Send(request);

        return Ok();
    }

    /// <summary>
    /// Обновит страну по идентификатору
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCountry([FromRoute] Guid id, [FromBody] UpdateCountryRequest request)
    {
        await sender.Send(new UpdateCountryCommand(
            id,
            request.Name,
            request.NameRu,
            request.NameUz,
            request.NameKa,
            request.Code,
            request.StateCode));

        return Ok();
    }

    /// <summary>
    /// Удалить страну по идентификатору
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountry([FromRoute] Guid id)
    {
        await sender.Send(new DeleteCountryCommand(id));

        return Ok();
    }
}
