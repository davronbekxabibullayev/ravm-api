namespace Ravm.Application.UseCases.RouteClassifications.Commands;

using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public record UpdateRouteClassificationCommand(
    Guid Id,
    string Name,
    string NameRu,
    string? NameUz,
    string? NameKa,
    string Code) : IRequest;

internal class UpdateRouteClassificationCommandHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateRouteClassificationCommand>
{
    private readonly IAppDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task Handle(UpdateRouteClassificationCommand request, CancellationToken cancellationToken)
    {
        var routeClassification = await GetRouteClassificationAsync(request.Id)
            ?? throw new NotFoundException(nameof(RouteClassification), request.Id);

        _mapper.Map(request, routeClassification);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    private Task<RouteClassification?> GetRouteClassificationAsync(Guid id)
    {
        return _dbContext.RouteClassifications
                    .AsTracking()
                    .FirstOrDefaultAsync(e => e.Id == id);
    }
}

