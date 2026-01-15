namespace Ravm.Application.UseCases.StopPoints.Commands;

using Microsoft.EntityFrameworkCore;
using Ravm.Domain.Enums;

public record UpdateStopPointCommand(
    Guid Id,
    string Name,
    string NameRu,
    string? NameUz,
    string? NameKa,
    string Code,
    StopPointPosition Position) : IRequest;

internal class UpdateStopPointCommandHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateStopPointCommand>
{
    private readonly IAppDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task Handle(UpdateStopPointCommand request, CancellationToken cancellationToken)
    {
        var stopPointId = request.Id;
        var stopPoint = await GetRouteStopAsync(stopPointId)
            ?? throw new NotFoundException(nameof(StopPoint), stopPointId);

        await CheckExistStopPointOrThrowAsync(stopPointId, request.Code);

        _mapper.Map(request, stopPoint);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private Task<StopPoint?> GetRouteStopAsync(Guid id)
    {
        return _dbContext.StopPoints
                    .AsTracking()
                    .FirstOrDefaultAsync(w => w.Id == id);
    }

    private async Task CheckExistStopPointOrThrowAsync(Guid id, string code)
    {
        if (await _dbContext.StopPoints.AnyAsync(x => x.Code == code && x.Id != id))
        {
            throw new AlreadyExistsException(nameof(StopPoint), code);
        }
    }
}
