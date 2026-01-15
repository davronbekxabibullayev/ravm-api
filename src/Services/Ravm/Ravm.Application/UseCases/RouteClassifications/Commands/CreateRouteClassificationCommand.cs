namespace Ravm.Application.UseCases.RouteClassifications.Commands;

public class CreateRouteClassificationCommand : IRequest
{
    public required string Name { get; set; }
    public required string NameRu { get; set; }
    public string? NameUz { get; set; }
    public string? NameKa { get; set; }
    public required string Code { get; set; }
}

internal class CreateRouteClassificationCommandHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<CreateRouteClassificationCommand>
{
    private readonly IAppDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task Handle(CreateRouteClassificationCommand request, CancellationToken cancellationToken)
    {
        var routeClassification = _mapper.Map<RouteClassification>(request);

        await _dbContext.RouteClassifications.AddAsync(routeClassification, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
