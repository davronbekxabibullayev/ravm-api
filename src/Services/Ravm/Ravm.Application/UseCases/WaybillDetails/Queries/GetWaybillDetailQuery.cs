namespace Ravm.Application.UseCases.WaybillDetails.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.UseCases.WaybillDetails.Models;

public record GetWaybillDetailQuery(Guid Id) : IRequest<WaybillDetailModel>;

internal sealed class GetWaybillDetailQueryHandler(IAppDbContext dbContext, IMapper mapper) :
    IRequestHandler<GetWaybillDetailQuery, WaybillDetailModel>
{
    public async Task<WaybillDetailModel> Handle(GetWaybillDetailQuery request, CancellationToken cancellationToken)
    {
        var waybillDetail = await dbContext.WaybillDetails
            .Include(x => x.Waybill!.Vehicle)
            .FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken)
            ?? throw new NotFoundException(nameof(WaybillDetail), request.Id);

        return mapper.Map<WaybillDetailModel>(waybillDetail);
    }
}
