namespace Ravm.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Ravm.Application.UseCases.Reports.StatDatas.Models;
using Ravm.Application.UseCases.Reports.StatDatas.Queries;

[Route("api/stat-datas")]
[ApiController]
[Authorize]

public class StatDatasController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<ActionResult<StatDatasModel>> GetStatDatas([FromQuery] DateTime from, [FromQuery] DateTime to)
    {
        return await _sender.Send(new GetStatDatasQuery()
        {
            From = from,
            To = to
        });
    }
}
