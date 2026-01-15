namespace Ravm.Application.UseCases.Reasons.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.UseCases.Reasons.Models;

public record GetReasonQuery(Guid Id) : IRequest<ReasonModel>;

internal class GetReasonQueryHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<GetReasonQuery, ReasonModel>
{
    public async Task<ReasonModel> Handle(GetReasonQuery request, CancellationToken cancellationToken)
    {
        var reason = await dbContext.Reasons
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Reason), request.Id);

        return mapper.Map<ReasonModel>(reason);
    }
}
