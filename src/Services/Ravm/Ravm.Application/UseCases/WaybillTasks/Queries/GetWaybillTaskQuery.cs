namespace Ravm.Application.UseCases.WaybillTasks.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.UseCases.WaybillTasks.Models;

public record GetWaybillTaskQuery(Guid Id) : IRequest<WaybillTaskModel>;

internal sealed class GetWaybillTaskQueryHandler(IAppDbContext dbContext, IMapper mapper) :
    IRequestHandler<GetWaybillTaskQuery, WaybillTaskModel>
{
    public async Task<WaybillTaskModel> Handle(GetWaybillTaskQuery request, CancellationToken cancellationToken)
    {
        var waybillTask = await dbContext.WaybillTasks
            .FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken)
            ?? throw new NotFoundException(nameof(WaybillTask), request.Id);

        return mapper.Map<WaybillTaskModel>(waybillTask);
    }
}
