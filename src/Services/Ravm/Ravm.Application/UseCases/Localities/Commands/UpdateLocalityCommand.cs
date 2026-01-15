namespace Ravm.Application.UseCases.Localities.Commands;

using Microsoft.EntityFrameworkCore;

public record UpdateLocalityCommand(
    Guid Id,
    string Name,
    string NameRu,
    string? NameKa,
    string? NameUz,
    string Code,
    Guid RegionId,
    Guid CityId,
    string? StateCode) : IRequest;
internal class UpdateLocalityCommandHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateLocalityCommand>
{
    private readonly IAppDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task Handle(UpdateLocalityCommand request, CancellationToken cancellationToken)
    {
        var locality = await GetLocalitiesAsync(request.Id)
            ?? throw new NotFoundException(nameof(Locality), request.Id);

        _mapper.Map(request, locality);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private Task<Locality?> GetLocalitiesAsync(Guid id)
    {
        return _dbContext.Localities
                    .AsTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
    }
}
