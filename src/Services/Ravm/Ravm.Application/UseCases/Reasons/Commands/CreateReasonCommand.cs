namespace Ravm.Application.UseCases.Reasons.Commands;

public record class CreateReasonCommand : IRequest
{
    public required string Name { get; set; }
    public required string NameRu { get; set; }
    public string? NameUz { get; set; }
    public string? NameKa { get; set; }
    public required string Code { get; set; }
}

internal class CreateReasonCommandHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<CreateReasonCommand>
{
    public async Task Handle(CreateReasonCommand command, CancellationToken cancellationToken)
    {
        var reason = mapper.Map<Reason>(command);

        await dbContext.Reasons.AddAsync(reason, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
