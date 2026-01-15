namespace Ravm.Application.UseCases.Reports.Drivers.Queries;

using Ravm.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Ravm.Application.Common.Models;
using Ravm.Application.UseCases.Employees.Models;
using Ravm.Application.UseCases.Reports.Drivers.Models;

public record GetReportDriversDataSummaryQuery(FilteringRequest FilteringRequest)
    : TimePeriodFilter, IRequest<PagedList<ReportDriverDataSummary>>
{
}

internal sealed class GetReportDriversDataSummaryQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetReportDriversDataSummaryQuery, PagedList<ReportDriverDataSummary>>
{
    public async Task<PagedList<ReportDriverDataSummary>> Handle(GetReportDriversDataSummaryQuery request, CancellationToken cancellationToken)
    {
        var employees = await GetEmployees(request);

        var waybillDoctorConclusions = await GetWaybillConclusions(request);

        if (waybillDoctorConclusions.Count == 0)
            return new PagedList<ReportDriverDataSummary>(new List<ReportDriverDataSummary>(), 0);

        var groupedWaybillDoctorConclusions = GroupWaybillConclusionsByDriver(waybillDoctorConclusions, employees);

        var result = GenerateReportDataSummary(groupedWaybillDoctorConclusions);

        return new PagedList<ReportDriverDataSummary>(result, result.Count);
    }

    private async Task<PagedList<Employee>> GetEmployees(GetReportDriversDataSummaryQuery request)
    {
        return await dbContext.Employees
            .OrderBy(d => d.FullName)
            .Where(e => e.OccupationGroupType == OccupationGroupType.Driver)
            .ToPagedListAsync(request.FilteringRequest);
    }

    private async Task<List<WaybillDoctorConclusion>> GetWaybillConclusions(GetReportDriversDataSummaryQuery request)
    {
        return await dbContext.WaybillDoctorConclusions
            .Include(wbdc => wbdc.WaybillDriver)
            .ThenInclude(wbd => wbd!.Employee)
            .Where(wbdc => wbdc.WaybillDetail!.ActualStartTime >= request.From
                        && wbdc.WaybillDetail.ActualEndTime <= request.To)
            .Include(wbdc => wbdc.WaybillDetail)
            .ToListAsync();
    }

    private List<(Employee Driver, IEnumerable<WaybillDetail?> WaybillDetails)> GroupWaybillConclusionsByDriver(List<WaybillDoctorConclusion> waybillDoctorConclusions, PagedList<Employee> employees)
    {
        var drivers = employees.Data;
        var driverIds = drivers.Select(d => d.Id);

        return waybillDoctorConclusions
            .Where(d => driverIds.Contains(d.WaybillDriver!.EmployeeId))
            .GroupBy(wbdc => wbdc.WaybillDriverId)
            .Select(group => new
            {
                WaybillDriverId = group.Key,
                WaybillDetails = group.Select(wbdc => wbdc.WaybillDetail),
                Driver = group.First()?.WaybillDriver?.Employee
            })
            .Select(item => (item.Driver!, item.WaybillDetails))
            .ToList();
    }

    private List<ReportDriverDataSummary> GenerateReportDataSummary(List<(Employee Driver, IEnumerable<WaybillDetail?> WaybillDetails)> groupedData)
    {
        var result = new List<ReportDriverDataSummary>();

        foreach (var item in groupedData)
        {
            var summary = GenerateSummaryForDriver(item.Driver, item.WaybillDetails);
            result.Add(summary);
        }

        foreach (var driver in groupedData.Select(item => item.Driver))
        {
            var summary = GenerateSummaryForDriver(driver, Enumerable.Empty<WaybillDetail?>());
            result.Add(summary);
        }

        return result;
    }

    private ReportDriverDataSummary GenerateSummaryForDriver(Employee driver, IEnumerable<WaybillDetail?> waybillDetails)
    {
        var totalRouteCount = waybillDetails.Count(wb => wb?.WaybillTaskId is null);
        var totalTaskCount = waybillDetails.Count(wb => wb?.WaybillTaskId is not null);
        var totalWorkedTimeOnRoute = CalculateTotalWorkedTime(waybillDetails.Where(wb => wb?.WaybillTaskId is null));
        var totalWorkedTimeOnTask = CalculateTotalWorkedTime(waybillDetails.Where(wb => wb?.WaybillTaskId is not null));
        var totalTraveledDistanceOnRoute = CalculateTotalDistance(waybillDetails.Where(wb => wb?.WaybillTaskId is null));
        var totalTraveledDistanceOnTask = CalculateTotalDistance(waybillDetails.Where(wb => wb?.WaybillTaskId is not null));

        var mappedDriver = mapper.Map<EmployeeModel>(driver);
        var summary = new ReportDriverDataSummary
        {
            Driver = mappedDriver,
            TaskCount = totalTaskCount,
            RouteCount = totalRouteCount,
            WorkedTimeTotalOnTask = totalWorkedTimeOnTask,
            WorkedTimeTotalOnRoute = totalWorkedTimeOnRoute,
            TraveledDistanceTotalOnTask = totalTraveledDistanceOnTask,
            TraveledDistanceTotalOnRoute = totalTraveledDistanceOnRoute,
        };

        return summary;
    }

    private static double CalculateTotalDistance(IEnumerable<WaybillDetail?> waybillDetails)
    {
        return waybillDetails.Sum(wb => wb!.ReturnSpeedometer - wb!.SpeedometerIndication);
    }

    private static TimeSpan CalculateTotalWorkedTime(IEnumerable<WaybillDetail?> waybillDetails)
    {
        return TimeSpan.FromTicks(waybillDetails.Sum(wb =>
        {
            if (wb?.ActualEndTime != null && wb?.ActualStartTime != null)
                return (wb.ActualEndTime.Value - wb.ActualStartTime.Value).Ticks;
            else
                return 0;
        }));
    }
}
