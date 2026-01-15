namespace Ravm.Application.UseCases.Reports.StatDatas.Queries;

using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ravm.Application.Common.Models;
using Ravm.Application.UseCases.Reports.StatDatas.Models;

public record GetStatDatasQuery : TimePeriodFilter, IRequest<StatDatasModel>
{
}

public class GetStatDatasQueryHandler(IAppDbContext dbContext) : IRequestHandler<GetStatDatasQuery, StatDatasModel>
{
    public async Task<StatDatasModel> Handle(GetStatDatasQuery request, CancellationToken cancellationToken)
    {
        var waybillCount = await dbContext.Waybills.Where(wb => wb.BeginDate >= request.PrevFrom &&
        wb.ExpireDate <= request.PrevTo).CountAsync(cancellationToken);

        var routeCount = await dbContext.Routes
        .Where(route => route.RouteOpenedDate >= request.PrevFrom
         && route.RouteOpenedDate <= request.PrevTo)
        .CountAsync(cancellationToken);

        var waybillTaskCount = await dbContext.WaybillTasks.Where(wbt => wbt.Date >= request.PrevFrom &&
        wbt.Date <= request.PrevTo).CountAsync(cancellationToken);

        var waybillDetailCount = await dbContext.WaybillDetails.Where(wbd => wbd.ActualStartTime >= request.PrevFrom
        && wbd.ActualEndTime <= request.PrevTo).CountAsync(cancellationToken);

        var statDatas = new StatDatasModel()
        {
            RouteCount = routeCount,
            WaybillCount = waybillCount,
            WaybillTaskCount = waybillTaskCount,
            WaybillDetailCount = waybillDetailCount,
        };

        return statDatas;
    }
}
