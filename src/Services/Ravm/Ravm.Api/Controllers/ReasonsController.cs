namespace Ravm.Api.Controllers;

using Ravm.Api.Models.Reasons;
using Ravm.Application.UseCases.Reasons.Commands;
using Ravm.Application.UseCases.Reasons.Models;
using Ravm.Application.UseCases.Reasons.Queries;

[Route("api/reasons")]
[ApiController]
[Authorize]
public class ReasonsController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    /// <summary>
    /// Получить причины
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<ReasonModel>>> GetReasons([FromQuery] GetReasonsQuery request)
    {
        return await _sender.Send(request);
    }

    /// <summary>
    /// Получить причину по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ReasonModel>> GetReason([FromRoute] Guid id)
    {
        return await _sender.Send(new GetReasonQuery(id));
    }

    /// <summary>
    /// Создать причину
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateReason([FromBody] CreateReasonCommand request)
    {
        await _sender.Send(request);

        return Ok();
    }

    /// <summary>
    /// Причина обновления по идентификатору
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReason([FromRoute] Guid id, [FromBody] UpdateReasonRequest request)
    {
        await _sender.Send(new UpdateReasonCommand(
            id,
            request.Name,
            request.NameRu,
            request.NameUz,
            request.NameKa,
            request.Code));

        return Ok();
    }

    /// <summary>
    /// Удалить причину по идентификатору
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReason([FromRoute] Guid id)
    {
        await _sender.Send(new DeleteReasonCommand(id));

        return Ok();
    }
}
