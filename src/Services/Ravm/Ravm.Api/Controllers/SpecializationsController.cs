namespace Ravm.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Ravm.Api.Models.Specialization;
using Ravm.Application.UseCases.Specializations.Commands;
using Ravm.Application.UseCases.Specializations.Models;
using Ravm.Application.UseCases.Specializations.Queries;

[Route("api/specializations")]
[ApiController]
[Authorize]
public class SpecializationsController(ISender sender) : Controller
{
    private readonly ISender _sender = sender;

    /// <summary>
    /// Получить коллекцию специализации
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<SpecializationModel>>> GetSpecializations([FromQuery] GetSpecializationsQuery query)
    {
        return await _sender.Send(query);
    }

    /// <summary>
    /// Получить специализацию по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<SpecializationModel>> GetSpecialization(Guid id)
    {
        return await _sender.Send(new GetSpecializationQuery(id));
    }

    /// <summary>
    /// Создать специализацию
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateSpecialization([FromBody] CreateSpecializationCommand command)
    {
        await _sender.Send(command);

        return Ok();
    }

    /// <summary>
    /// Обновить специализацию
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSpecialization([FromRoute] Guid id, [FromBody] UpdateSpecializationRequest request)
    {
        await _sender.Send(new UpdateSpecializationCommand(
            id,
            request.Name,
            request.NameRu,
            request.NameUz,
            request.NameKa,
            request.Code));

        return Ok();
    }

    /// <summary>
    /// Удалить специализацию по идентификатору
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSpecialization([FromRoute] Guid id)
    {
        await _sender.Send(new DeleteSpecializationCommand(id));

        return Ok();
    }
}
