namespace Ravm.Application.UseCases.Specializations.Commands;

public record CreateSpecializationCommand : IRequest
{
    public required string Name { get; set; }
    public required string NameRu { get; set; }
    public string? NameUz { get; set; }
    public string? NameKa { get; set; }
    public required string Code { get; set; }
}

internal class CreateSpecializationCommandHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<CreateSpecializationCommand>
{
    private readonly IAppDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    public async Task Handle(CreateSpecializationCommand request, CancellationToken cancellationToken)
    {
        var specialization = _mapper.Map<Specialization>(request);

        await _dbContext.Specializations.AddAsync(specialization, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
