namespace Ravm.Application.UseCases.Reports.Vehicles.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.Common.Models;
using Ravm.Application.UseCases.Reports.Vehicles.Models;
using Ravm.Domain.Enums;

public record GetVehicleDetailsQuery(Guid VehicleId, FilteringRequest FilteringRequest) : TimePeriodFilter, IRequest<PagedList<VehicleDetailModel>>;

internal sealed class GetVehicleDetailsQueryHandler(IAppDbContext dbContext) :
    IRequestHandler<GetVehicleDetailsQuery, PagedList<VehicleDetailModel>>
{
    public async Task<PagedList<VehicleDetailModel>> Handle(GetVehicleDetailsQuery request, CancellationToken cancellationToken)
    {
        var waybills = await GetWaybills(dbContext, request);

        var vehicleDetails = await GetVehicleDetails(dbContext, waybills);

        return new PagedList<VehicleDetailModel>(vehicleDetails, vehicleDetails.Count);
    }

    private static Task<List<Waybill>> GetWaybills(IAppDbContext dbContext, GetVehicleDetailsQuery request)
    {
        return dbContext.Waybills
            .Where(x => x.VehicleId.Equals(request.VehicleId) && x.BeginDate >= request.From && x.ExpireDate <= request.To)
            .Include(x => x.Route)
            .Include(x => x.WaybillDetails.Where(a => a.IsVehicleOk))
               .ThenInclude(wbd => wbd.MechanicConclusions)
            .Include(x => x.WaybillDetails.Where(a => a.IsVehicleOk))
               .ThenInclude(wbd => wbd.WaybillFuels)
            .ToListAsync();
    }

    private static async Task<List<VehicleDetailModel>> GetVehicleDetails(IAppDbContext dbContext, List<Waybill> waybills)
    {
        var vehicleDetails = new List<VehicleDetailModel>();

        foreach (var waybill in waybills)
        {
            var routeName = waybill.Route?.Name ?? "Unknown";
            var waybillDetails = await dbContext.WaybillDetails
                .Where(x => x.WaybillId.Equals(waybill.Id) && x.IsVehicleOk)
                .Include(x => x.WaybillFuels)
                .Include(x => x.WaybillTask)
                .Select(wd => new
                {
                    Date = waybill.CreatedAt.UtcDateTime,
                    WaybillNumber = waybill.Number,
                    RouteName = routeName,
                    Distance = wd.ReturnSpeedometer - wd.SpeedometerIndication,
                    TaskNumber = wd.WaybillTask!.Number ?? "Unknown",
                    Fuel = GetFuelAmount(wd.MechanicConclusions, wd.WaybillFuels)
                })
                .ToListAsync();

            vehicleDetails.AddRange(waybillDetails.Select(x => new VehicleDetailModel
            {
                Date = x.Date,
                WaybillNumber = x.WaybillNumber,
                RouteName = x.RouteName,
                Distance = x.Distance,
                TaskNumber = x.TaskNumber,
                Fuel = x.Fuel
            }));
        }

        return vehicleDetails;
    }

    private static double GetFuelAmount(ICollection<WaybillMechanicConclusion> mechanicConclusions, ICollection<WaybillFuel> waybillFuels)
    {
        var waybillFuel = waybillFuels.Sum(wf => wf.Amount);

        var mechanicFuels = mechanicConclusions
            .GroupBy(mc => mc.MechanicConclusionType)
            .ToDictionary(g => g.Key, g => g.Sum(mc => mc.FuelAmount));

        var putTypeMechanicFuel = mechanicFuels.GetValueOrDefault(MechanicConclusionType.put, 0.0);
        var acceptTypeMechanicFuel = mechanicFuels.GetValueOrDefault(MechanicConclusionType.acceptance, 0.0);

        var allFuelAmount = waybillFuel + putTypeMechanicFuel - acceptTypeMechanicFuel;
        return allFuelAmount;
    }
}

