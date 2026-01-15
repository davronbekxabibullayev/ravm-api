namespace Ravm.Api.Controllers;

using Ravm.Api.Models.WaybillDrivers;
using Ravm.Application.UseCases.WaybillDrivers.Commands;
using Ravm.Application.UseCases.WaybillDrivers.Models;
using Ravm.Application.UseCases.WaybillDrivers.Queries;

[Route("api/waybill-drivers")]
[ApiController]
[Authorize]
public class WaybillDriversController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Получить список путевой лист и водитель
    /// </summary>
    [HttpGet]
    public async Task<PagedList<WaybillDriverModel>> GetWaybillDrivers([FromQuery] GetWaybillDriversQuery query)
    {
        return await sender.Send(query);
    }

    /// <summary>
    /// Получить путевой лист и водитель по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<WaybillDriverModel> GetWaybillDriver([FromRoute] Guid id)
    {
        return await sender.Send(new GetWaybillDriverQuery(id));
    }

    /// <summary>
    /// Создать новую путевой лист и водитель
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateWaybillDriver([FromBody] CreateWaybillDriverCommand command)
    {
        await sender.Send(command);

        return Ok();
    }

    /// <summary>
    /// Обновит путевой лист и водитель
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWaybillDriver([FromRoute] Guid id, [FromBody] UpdateWaybillDriverRequest request)
    {
        await sender.Send(new UpdateWaybillDriverCommand
            (
            id,
            request.EmployeeId,
            request.WaybillId,
            request.WaybillDriverRole
            ));

        return Ok();
    }

    /// <summary>
    /// Удалить путевой лист и водитель по идентификатору
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWaybillDriver([FromRoute] Guid id)
    {
        await sender.Send(new DeleteWaybillDriverCommand(id));

        return Ok();
    }
}
