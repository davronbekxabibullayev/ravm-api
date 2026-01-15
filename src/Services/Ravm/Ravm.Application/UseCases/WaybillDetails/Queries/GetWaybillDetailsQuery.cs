namespace Ravm.Application.UseCases.WaybillDetails.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.UseCases.WaybillDetails.Models;

public record GetWaybillDetailsQuery(Guid? WaybillId) : FilteringRequest, IRequest<PagedList<WaybillDetailModel>>;

internal sealed class GetWaybillDetailsQueryHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<GetWaybillDetailsQuery, PagedList<WaybillDetailModel>>
{
    public async Task<PagedList<WaybillDetailModel>> Handle(GetWaybillDetailsQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.WaybillDetails
            .Include(a => a.WaybillTask)
            .Include(a => a.MechanicConclusions)
              .ThenInclude(mc => mc.ReceivedDriver)
            .Include(a => a.MechanicConclusions)
              .ThenInclude(mc => mc.ReturnedDriver)
            .Include(a => a.WaybillDoctorConclusions)
              .ThenInclude(wbd => wbd.WaybillDriver)
                .ThenInclude(a => a!.Employee)
            .Include(a => a.WaybillTask)
            .AsQueryable();

        if (request.WaybillId.HasValue)
        {
            query = query.Where(x => x.WaybillId == request.WaybillId);
        }

        return await query
            .ProjectTo<WaybillDetailModel>(mapper.ConfigurationProvider)
            .ToPagedListAsync(request);
    }
}
