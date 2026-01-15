namespace Ravm.Api.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ravm.Api.Models.Organizations;
using Ravm.Application.UseCases.Organizations.Commands;
using Ravm.Application.UseCases.Organizations.Models;
using Ravm.Application.UseCases.Organizations.Queries;

[Route("api/organizations")]
[ApiController]
[Authorize]
public class OrganizationsController(ISender sender, IMapper mapper) : ControllerBase
{
    private readonly IMapper _mapper = mapper;
    private readonly ISender _sender = sender;

    /// <summary>
    /// Получить организацию по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<OrganizationModel>> GetOrganization(Guid id)
    {
        return await _sender.Send(new GetOrganizationQuery(id));
    }

    /// <summary>
    /// Получить организации с фильтрами
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<OrganizationModel>>> GetOrganizations([FromQuery] GetOrganizationsQuery query)
    {
        return await _sender.Send(query);
    }

    /// <summary>
    /// Создать организацию
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationCommand command)
    {
        await _sender.Send(command);
        return Ok();
    }

    /// <summary>
    /// Обновить организацию по идентификатору
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrganization(Guid id, [FromBody] UpdateOrganizationRequest request)
    {
        var updateModel = _mapper.Map<UpdateOrganizationCommand>(request);
        updateModel.Id = id;
        await _sender.Send(updateModel);
        return Ok();
    }

    /// <summary>
    /// Удалить организации по идентификатору
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrganization(Guid id)
    {
        await _sender.Send(new DeleteOrganizationCommand(id));
        return Ok();
    }

}
