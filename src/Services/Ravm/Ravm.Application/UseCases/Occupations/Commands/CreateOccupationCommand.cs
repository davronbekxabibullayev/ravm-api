namespace Ravm.Application.UseCases.Occupations.Commands;
public record CreateOccupationCommand : IRequest
{
    public required string Name { get; set; }
    public required string NameRu { get; set; }
    public string? NameUz { get; set; }
    public string? NameKa { get; set; }
    public required string? Code { get; set; }
}

internal class CreateOccupationCommanHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<CreateOccupationCommand>
{
    private readonly IAppDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    public async Task Handle(CreateOccupationCommand request, CancellationToken cancellationToken)
    {
        var occupation = _mapper.Map<Domain.Models.Occupation>(request);

        await _dbContext.Occupations.AddAsync(occupation, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
