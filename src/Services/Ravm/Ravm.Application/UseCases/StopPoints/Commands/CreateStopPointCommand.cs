namespace Ravm.Application.UseCases.StopPoints.Commands;

using Microsoft.EntityFrameworkCore;
using Ravm.Domain.Enums;

public record CreateStopPointCommand : IRequest
{
    public required string Name { get; set; }
    public required string NameRu { get; set; }
    public string? NameUz { get; set; }
    public string? NameKa { get; set; }
    public required string Code { get; set; }
    public StopPointPosition Position { get; set; }
}

internal class CreateStopPointCommandHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<CreateStopPointCommand>
{
    private readonly IAppDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task Handle(CreateStopPointCommand request, CancellationToken cancellationToken)
    {
        await CheckExistStopPointOrThrowAsync(request.Code);

        var stopPoint = _mapper.Map<StopPoint>(request);

        await _dbContext.StopPoints.AddAsync(stopPoint, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task CheckExistStopPointOrThrowAsync(string code)
    {
        if (await _dbContext.StopPoints.AnyAsync(x => x.Code == code))
        {
            throw new AlreadyExistsException(nameof(StopPoint), code);
        }
    }
}
