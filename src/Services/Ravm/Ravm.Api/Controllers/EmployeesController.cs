namespace Ravm.Api.Controllers;

using AutoMapper;
using Ravm.Api.Models.Employees;
using Ravm.Application.UseCases.Employees.Commands;
using Ravm.Application.UseCases.Employees.Models;
using Ravm.Application.UseCases.Employees.Queries;

[Route("api/employees")]
[ApiController]
[Authorize]
public class EmployeesController(ISender sender, IMapper mapper) : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Получить список сотрудников
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<EmployeeModel>>> GetEmployees([FromQuery] GetEmployeesQuery query)
    {
        return await _sender.Send(query);
    }

    /// <summary>
    /// Получить данные сотрудника по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeModel>> GetEmployee([FromRoute] Guid id)
    {
        return await _sender.Send(new GetEmployeeQuery(id));
    }

    /// <summary>
    /// Добавление нового сотрудника
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand cmd)
    {
        await _sender.Send(cmd);

        return Ok();
    }

    /// <summary>
    /// Обновить данные сотрудника по идентификатору
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, [FromBody] UpdateEmployeeRequest request)
    {
        var updateModel = _mapper.Map<UpdateEmployeeCommand>(request);
        updateModel.Id = id;
        await _sender.Send(updateModel);
        return Ok();
    }

    /// <summary>
    /// Удалить сотрудника по идентификатору
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
    {
        await _sender.Send(new DeleteEmployeeCommand(id));

        return Ok();
    }
}
