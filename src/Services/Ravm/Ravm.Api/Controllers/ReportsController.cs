namespace Ravm.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Ravm.Api.Models.Reports;
using Ravm.Application.UseCases.Reports.Drivers.Models;
using Ravm.Application.UseCases.Reports.Drivers.Queries;
using Ravm.Application.UseCases.Reports.Vehicles.Models;
using Ravm.Application.UseCases.Reports.Vehicles.Queries;

[Route("api/reports")]
[ApiController]
[Authorize]

public class ReportsController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    /// <summary>
    /// Получите общие отчеты о данных водителя
    /// </summary>
    [HttpGet("driver-summary")]
    public async Task<ActionResult<PagedList<ReportDriverDataSummary>>> GetDriverReports([FromQuery] FilteringRequest query, [FromQuery] GetReportsByPeriodRequest request)
    {
        return await _sender.Send(new GetReportDriversDataSummaryQuery(query)
        {
            From = request.From,
            To = request.To
        });
    }

    /// <summary>
    /// Получайте подробные отчеты о данных водителя
    /// </summary>
    [HttpGet("driver/{employeeId}/details")]
    public async Task<ActionResult<PagedList<ReportDriverDetailDatas>>> GetDriverReportDetails([FromRoute] Guid employeeId, [FromQuery] FilteringRequest query, [FromQuery] GetReportsByPeriodRequest request)
    {
        return await _sender.Send(new GetReportDriverDetailDatasQuery(employeeId, query)
        {
            From = request.From,
            To = request.To
        });
    }

    /// <summary>
    /// Получите общие автомобильные отчеты
    /// </summary>
    [HttpGet("vehicle-summary")]
    public async Task<ActionResult<PagedList<ReportVehicleDataSummary>>> GetVehiclesReports([FromQuery] FilteringRequest query, [FromQuery] GetReportsByPeriodRequest request)
    {
        return await _sender.Send(new GetReportVehiclesDataSummaryQuery(query)
        {
            From = request.From,
            To = request.To
        });
    }

    /// <summary>
    /// Получайте подробные отчеты об автомобиле
    /// </summary>
    [HttpGet("vehicles/{vehicleId}/details")]
    public async Task<ActionResult<PagedList<VehicleDetailModel>>> GetVehicleReportDetails([FromRoute] Guid vehicleId, [FromQuery] FilteringRequest query, [FromQuery] GetReportsByPeriodRequest request)
    {
        return await _sender.Send(new GetVehicleDetailsQuery(vehicleId, query)
        {
            From = request.From,
            To = request.To
        });
    }
}
