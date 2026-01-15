namespace Ravm.Application.UseCases.WaybillDrivers.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.UseCases.WaybillDrivers.Models;

public record GetWaybillDriverQuery(Guid Id) : IRequest<WaybillDriverModel>;

internal sealed class GetWaybillDriverQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetWaybillDriverQuery, WaybillDriverModel>
{
    public async Task<WaybillDriverModel> Handle(GetWaybillDriverQuery request, CancellationToken cancellationToken)
    {
        var waybillDriver = await dbContext.WaybillDrivers
            .Include(a => a.Employee)
            .FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken)
            ?? throw new NotFoundException(nameof(WaybillDriver), request.Id);

        return mapper.Map<WaybillDriverModel>(waybillDriver);
    }
}
