namespace Ravm.Application.UseCases.Regions.Commands;

public record CreateRegionCommand : IRequest
{
    public required string Name { get; set; }

    public required string NameRu { get; set; }

    public string? NameUz { get; set; }

    public string? NameKa { get; set; }

    public Guid CountryId { get; set; }

    public required string Code { get; set; }

    public string? StateCode { get; set; }
}

internal class CreateRegionCommandHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<CreateRegionCommand>
{
    private readonly IMapper _mapper = mapper;
    private readonly IAppDbContext _dbContext = dbContext;

    public async Task Handle(CreateRegionCommand request, CancellationToken cancellationToken)
    {
        var region = _mapper.Map<Region>(request);

        await _dbContext.Regions.AddAsync(region, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

    }
}
