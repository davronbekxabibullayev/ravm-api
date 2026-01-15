namespace Ravm.Application.UseCases.Reports.Drivers.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.Common.Models;
using Ravm.Application.UseCases.Reports.Drivers.Models;

public record GetReportDriverDetailDatasQuery(Guid EmployeeId, FilteringRequest FilteringRequest)
    : TimePeriodFilter, IRequest<PagedList<ReportDriverDetailDatas>>;

internal sealed class GetReportDriverDataDetailsQueryHandler(IAppDbContext dbContext)
    : IRequestHandler<GetReportDriverDetailDatasQuery, PagedList<ReportDriverDetailDatas>>
{
    public async Task<PagedList<ReportDriverDetailDatas>> Handle(GetReportDriverDetailDatasQuery request, CancellationToken cancellationToken)
    {
        var waybillDoctorConclusions = GetWaybillDoctorConclusions(dbContext, request);

        var driverDetails = await waybillDoctorConclusions!.ToPagedListAsync<WaybillDetail>(request.FilteringRequest);

        var result = GetDriverDetails(driverDetails);

        return new PagedList<ReportDriverDetailDatas>(result, result.Count);
    }

    private static IQueryable<WaybillDetail?> GetWaybillDoctorConclusions(IAppDbContext dbContext, GetReportDriverDetailDatasQuery request)
    {
        return dbContext.WaybillDoctorConclusions
                        .Include(conclusion => conclusion.WaybillDetail)
                            .ThenInclude(detail => detail!.Waybill)
                                .ThenInclude(waybill => waybill!.Vehicle)
                                    .ThenInclude(vc => vc!.VehicleModel)
                                        .ThenInclude(vcm => vcm!.VehicleMark)
                        .Include(conclusion => conclusion.WaybillDetail)
                            .ThenInclude(detail => detail!.Waybill)
                                .ThenInclude(waybill => waybill!.Route)
                        .Include(conclusion => conclusion.WaybillDetail)
                            .ThenInclude(detail => detail!.ReceivedMechanic)
                        .Where(conclusion => conclusion.WaybillDriver!.EmployeeId.Equals(request.EmployeeId))
                        .Select(conclusion => conclusion.WaybillDetail)
                            .Where(detail => detail!.ActualStartTime >= request.From
                             && detail.ActualEndTime <= request.To);
    }

    private static List<ReportDriverDetailDatas> GetDriverDetails(PagedList<WaybillDetail> driverDetails)
    {
        return driverDetails.Data.Select(detail => new ReportDriverDetailDatas
        {
            VehicleGarageNumber = detail!.Waybill!.Vehicle!.GarageNumber,
            VehicleMark = detail.Waybill.Vehicle.VehicleModel!.VehicleMark!.Name,
            VehicleStateNumbers = detail.Waybill.Vehicle.StateNumber,
            RouteNumber = detail.Waybill.Route?.Number,
            NotesAnotherRoute = detail?.Waybill?.Route?.Note,
            Date = detail!.Date,
            WorkedHoursOnRoute = detail?.WaybillTaskId is null ? detail!.ActualEndTime - detail.ActualStartTime : TimeSpan.Zero,
            WorkedHoursOnTask = detail.WaybillTaskId != null ? detail.ActualEndTime - detail.ActualStartTime : TimeSpan.Zero,
            КeserveDutyTime = detail.КeserveDutyTime,
            UnjustifiedTime = detail.UnjustifiedTime,
            IdleTime = detail.IdleTime,
            NightOrHolidayTime = detail.NightOrHolidayTime,
            ScheduledRoutesCount = detail.ScheduledRoutesCount,
            ActuallyRoutesCount = detail.ActuallyRoutesCount,
            TraveledDistanceTask = detail.WaybillTaskId != null ? detail.ReturnSpeedometer - detail.SpeedometerIndication : 0,
            TraveledDistanceRoute = detail.WaybillTaskId is null ? detail.ReturnSpeedometer - detail.SpeedometerIndication : 0,
            ReceivedMechanicName = detail.ReceivedMechanic?.FullName ?? "N/A"
        }).ToList();
    }
}
