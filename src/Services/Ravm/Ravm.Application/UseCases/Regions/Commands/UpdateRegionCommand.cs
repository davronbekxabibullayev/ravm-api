namespace Ravm.Application.UseCases.Regions.Commands;

using Microsoft.EntityFrameworkCore;

public record UpdateRegionCommand(
    Guid Id,
    string Name,
    string NameRu,
    string? NameUz,
    string? NameKa,
    Guid CountryId,
    string Code,
    string? StateCode) : IRequest;

internal class UpdateRegionCommandHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateRegionCommand>
{
    private readonly IAppDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task Handle(UpdateRegionCommand request, CancellationToken cancellationToken)
    {
        var region = await GetRegionAsync(request.Id)
            ?? throw new NotFoundException(nameof(Region), request.Id);

        _mapper.Map(request, region);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private Task<Region?> GetRegionAsync(Guid Id)
    {
        return _dbContext.Regions
            .AsTracking()
            .FirstOrDefaultAsync(x => x.Id == Id);
    }
}
