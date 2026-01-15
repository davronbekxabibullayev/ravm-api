namespace Ravm.Application.UseCases.Reports.Vehicles.Queries;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ravm.Application.Common.Models;
using Ravm.Application.UseCases.Vehicles.Models;
using Ravm.Application.UseCases.Reports.Vehicles.Models;
using Microsoft.EntityFrameworkCore.Query;
using Ravm.Domain.Enums;

public record GetReportVehiclesDataSummaryQuery(FilteringRequest FilteringRequest)
    : TimePeriodFilter, IRequest<PagedList<ReportVehicleDataSummary>>;

internal sealed class GetReportVehicleDataSummaryQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetReportVehiclesDataSummaryQuery, PagedList<ReportVehicleDataSummary>>
{
    public async Task<PagedList<ReportVehicleDataSummary>> Handle(GetReportVehiclesDataSummaryQuery request, CancellationToken cancellationToken)
    {
        var vehicleDatas = await GetVehicleDatas(dbContext, request);

        var waybillDatas = GetWaybillDatas(dbContext, request);

        if (waybillDatas.ToList().Count == 0)
            return new PagedList<ReportVehicleDataSummary>(new List<ReportVehicleDataSummary>(), 0);

        var reportData = GetReportDatas(mapper, vehicleDatas.Data, waybillDatas);

        return new PagedList<ReportVehicleDataSummary>(reportData, reportData.Count);
    }

    private static IIncludableQueryable<Waybill, ICollection<WaybillMechanicConclusion>> GetWaybillDatas(IAppDbContext dbContext, GetReportVehiclesDataSummaryQuery request)
    {
        return dbContext.Waybills
                    .Where(a => a.BeginDate >= request.From && a.ExpireDate <= request.To)
                    .Include(a => a.WaybillFuels)
                    .Include(a => a.WaybillTasks)
                    .Include(a => a.WaybillDetails)
                      .ThenInclude(wbd => wbd.MechanicConclusions);
    }

    private static async Task<PagedList<Vehicle>> GetVehicleDatas(IAppDbContext dbContext, GetReportVehiclesDataSummaryQuery request)
    {
        return await dbContext.Vehicles
                   .Include(v => v.VehicleModel)
                   .ThenInclude(vm => vm!.VehicleMark)
                   .Include(v => v.Organization)
                   .ToPagedListAsync(request.FilteringRequest);
    }

    private static List<ReportVehicleDataSummary> GetReportDatas(IMapper mapper, List<Vehicle> vehicles, IIncludableQueryable<Waybill, ICollection<WaybillMechanicConclusion>> waybillDatas)
    {
        var reportData = new List<ReportVehicleDataSummary>();

        foreach (var vehicle in vehicles)
        {
            var vehicleWaybillDetails = waybillDatas.Where(wb => wb.VehicleId.Equals(vehicle.Id))
                                     .SelectMany(wb => wb.WaybillDetails);

            var vehicleWaybillFuel = waybillDatas.Where(wb => wb.VehicleId.Equals(vehicle.Id))
                                     .SelectMany(wb => wb.WaybillFuels);

            var mileage = CalculateMileageForVehicle(vehicleWaybillDetails);
            var fuelAmount = GetFuelAmount(vehicleWaybillDetails, vehicleWaybillFuel);

            var mappedVehicle = mapper.Map<VehicleItemDto>(vehicle);
            reportData.Add(new ReportVehicleDataSummary
            {
                Vehicle = mappedVehicle,
                Mileage = mileage,
                FuelAmount = fuelAmount
            });
        }

        return reportData;
    }

    private static double GetFuelAmount(IQueryable<WaybillDetail> vehicleWaybillDetails, IQueryable<WaybillFuel> vehicleWaybillFuel)
    {
        var waybillFuel = CalculateWaybillFuelForVehicle(vehicleWaybillFuel);

        var mechanicFuels = vehicleWaybillDetails
            .SelectMany(wbd => wbd.MechanicConclusions)
            .GroupBy(mc => mc.MechanicConclusionType)
            .ToDictionary(g => g.Key, g => g.Sum(mc => mc.FuelAmount));

        var putTypeMechanicFuel = mechanicFuels.GetValueOrDefault(MechanicConclusionType.put, 0.0);
        var acceptTypeMechanicFuel = mechanicFuels.GetValueOrDefault(MechanicConclusionType.acceptance, 0.0);

        var allFuelAmount = waybillFuel + putTypeMechanicFuel - acceptTypeMechanicFuel;
        return allFuelAmount;
    }

    private static double CalculateMileageForVehicle(IQueryable<WaybillDetail> waybillDetails)
    {
        var totalMileage = 0.0;

        foreach (var detail in waybillDetails)
        {
            totalMileage += detail.ReturnSpeedometer - detail.SpeedometerIndication;
        }

        return totalMileage;
    }

    private static double CalculateWaybillFuelForVehicle(IQueryable<WaybillFuel> waybillFuels)
    {
        var totalFuel = 0.0;

        foreach (var detail in waybillFuels)
        {
            totalFuel += detail.Amount;
        }

        return totalFuel;
    }
}
