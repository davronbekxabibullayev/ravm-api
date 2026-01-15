namespace Ravm.Application.UseCases.Localities.Commands;
public record CreateLocalityCommand : IRequest
{
    public required string Name { get; set; }
    public required string NameRu { get; set; }
    public string? NameUz { get; set; }
    public string? NameKa { get; set; }
    public Guid? RegionId { get; set; }
    public Guid? CityId { get; set; }
    public required string Code { get; set; }
    public string? StateCode { get; set; }
}

internal class CreateLocalityCommanHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<CreateLocalityCommand>
{
    private readonly IAppDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    public async Task Handle(CreateLocalityCommand request, CancellationToken cancellationToken)
    {
        var loclity = _mapper.Map<Locality>(request);

        await _dbContext.Localities.AddAsync(loclity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
