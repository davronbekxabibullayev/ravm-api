namespace Ravm.Application.UseCases.StopPoints.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.UseCases.StopPoints.Models;

public record GetStopPointQuery(Guid Id) : IRequest<StopPointModel>;

internal class GetStopPointQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetStopPointQuery, StopPointModel>
{
    private readonly IAppDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<StopPointModel> Handle(GetStopPointQuery request, CancellationToken cancellationToken)
    {
        var stopPoint = await _dbContext.StopPoints
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(StopPointModel), request.Id);

        return _mapper.Map<StopPointModel>(stopPoint);
    }
}
