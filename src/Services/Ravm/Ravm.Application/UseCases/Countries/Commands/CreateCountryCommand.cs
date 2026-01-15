namespace Ravm.Application.UseCases.Countries.Commands;

public record CreateCountryCommand : IRequest
{
    public required string Name { get; set; }
    public required string NameRu { get; set; }
    public string? NameUz { get; set; }
    public string? NameKa { get; set; }
    public required string Code { get; set; }
    public string? StateCode { get; set; }
}

internal class CreateCountryCommandHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<CreateCountryCommand>
{
    public async Task Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        var country = mapper.Map<Country>(request);

        await dbContext.Countries.AddAsync(country, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
