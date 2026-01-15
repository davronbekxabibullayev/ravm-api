namespace Ravm.Application.UseCases.Reasons.Commands;

using Microsoft.EntityFrameworkCore;

public record DeleteReasonCommand(Guid Id) : IRequest;

internal class DeleteReasonCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteReasonCommand>
{
    public async Task Handle(DeleteReasonCommand command, CancellationToken cancellationToken)
    {
        var reason = await dbContext.Reasons
            .Where(w => w.Id == command.Id)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.IsDeleted, true), cancellationToken);

        if (reason == 0)
        {
            throw new NotFoundException(nameof(Reason), command.Id);
        }
    }
}
