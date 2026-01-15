namespace Ravm.Api.Controllers;

using Ravm.Api.Models.Accounts;
using Ravm.Api.Services;
using Ravm.Application.UseCases.Accounts.Commands;
using Ravm.Application.UseCases.Accounts.Models;
using Ravm.Application.UseCases.Accounts.Queries;

[Route("api/accounts")]
[ApiController]
[Authorize]
public class AccountsController(ISender sender, IUserService userService) : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IUserService _userService = userService;

    /// <summary>
    /// Получить аккаунт по идентификатору сотрудника
    /// </summary>
    [HttpGet("{employeeId}")]
    public async Task<ActionResult<AccountModel>> GetAccount([FromRoute] Guid employeeId)
    {
        return await _sender.Send(new GetEmployeeAccountQuery(employeeId));
    }

    /// <summary>
    /// Создание аккаунта сотрудника    
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] CreateEmployeeAccountCommand request)
    {
        var result = await _sender.Send(request);

        return Ok(result);
    }

    /// <summary>
    /// Обновить аккаунта сотрудника
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAccount([FromRoute] Guid id, [FromBody] UpdateEmployeeAccountRequest request)
    {
        var result = await _sender.Send(new UpdateEmployeeAccountCommand(
            id,
            request.Email,
            request.PhoneNumber,
            request.Avatar)
        {
            Roles = request.Roles
        });

        return Ok(result);
    }

    /// <summary>
    /// Обновить сотрудника по идентификатору пользователя
    /// </summary>
    [HttpPut("{id}/employee")]
    public async Task<IActionResult> UpdateUserEmployee([FromRoute] Guid id, [FromBody] UpdateEmployeeByUserIdRequest request)
    {
        await _sender.Send(new UpdateEmployeeByUserIdCommand(
        id,
        request.FirstName,
        request.LastName,
        request.MiddleName,
        request.Gender,
        request.Pin,
        request.BirthDate));

        return Ok();
    }

    /// <summary>
    /// Обновить аккаунта сотрудника
    /// </summary>
    [HttpPut("password")]
    public async Task<IActionResult> UpdateAccountPassword([FromBody] UpdateEmployeeAccountPasswordRequest request)
    {
        await _userService.UpdateEmployeeAccountPassword(request);

        return Ok();
    }

    /// <summary>
    /// Удалить аккаунты сотрудников
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAccount([FromRoute] Guid id)
    {
        await _sender.Send(new DeleteEmployeeAccountCommand(id));

        return Ok();
    }
}
