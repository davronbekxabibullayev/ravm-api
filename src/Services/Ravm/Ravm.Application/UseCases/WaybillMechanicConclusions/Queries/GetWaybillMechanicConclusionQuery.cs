namespace Ravm.Application.UseCases.WaybillMechanicConclusions.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.UseCases.WaybillMechanicConclusions.Models;

public record GetWaybillMechanicConclusionQuery(Guid Id) : IRequest<WaybillMechanicConclusionModel>;

internal sealed class GetWaybillMechanicConclusionQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetWaybillMechanicConclusionQuery, WaybillMechanicConclusionModel>
{
    public async Task<WaybillMechanicConclusionModel> Handle(GetWaybillMechanicConclusionQuery request,
        CancellationToken cancellationToken)
    {
        var waybillMechanicConclusion = await dbContext.WaybillMechanicConclusions
            .Include(x => x.WaybillDetail)
            .ThenInclude(a => a!.Waybill!.Vehicle)
            .FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken)
            ?? throw new NotFoundException(nameof(WaybillMechanicConclusion), request.Id);

        return mapper.Map<WaybillMechanicConclusionModel>(waybillMechanicConclusion);
    }
}
