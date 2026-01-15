namespace Ravm.Application.UseCases.WaybillDetails.Queries;

using System.Threading;
using System.Threading.Tasks;
using Ravm.Application.Common.Models;
using Ravm.Application.UseCases.WaybillDetails.Models;

public record GetWaybillDetailsByPeriodQuery(FilteringRequest FilteringRequest) : TimePeriodFilter, IRequest<PagedList<WaybillDetailModel>>;

internal sealed class GetWaybillDetailsByPeriodQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetWaybillDetailsByPeriodQuery, PagedList<WaybillDetailModel>>
{
    public async Task<PagedList<WaybillDetailModel>> Handle(GetWaybillDetailsByPeriodQuery request, CancellationToken cancellationToken)
    {
        var waybillDetails = dbContext.WaybillDetails
            .Where(x => x.Date >= request.From && x.Date <= request.To);

        return await waybillDetails
            .ProjectTo<WaybillDetailModel>(mapper.ConfigurationProvider)
            .ToPagedListAsync(request.FilteringRequest);
    }
}
