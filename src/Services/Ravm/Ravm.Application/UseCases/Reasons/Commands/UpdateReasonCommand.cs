namespace Ravm.Application.UseCases.Reasons.Commands;

using Microsoft.EntityFrameworkCore;

public record UpdateReasonCommand(
    Guid Id,
    string Name,
    string NameRu,
    string? NameUz,
    string? NameKa,
    string Code) : IRequest;

internal class UpdateReasonCommandHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<UpdateReasonCommand>
{
    public async Task Handle(UpdateReasonCommand command, CancellationToken cancellationToken)
    {
        var reason = await GetReasonAsync(command.Id)
            ?? throw new NotFoundException(nameof(Reason), command.Id);

        mapper.Map(command, reason);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private Task<Reason?> GetReasonAsync(Guid id)
    {
        return dbContext.Reasons
                    .AsTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
    }
}
