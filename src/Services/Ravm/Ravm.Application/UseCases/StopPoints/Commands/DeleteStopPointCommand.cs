namespace Ravm.Application.UseCases.StopPoints.Commands;

using Microsoft.EntityFrameworkCore;

public record DeleteStopPointCommand(Guid Id) : IRequest;

internal class DeleteStopPointCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteStopPointCommand>
{
    private readonly IAppDbContext _dbContext = dbContext;

    public async Task Handle(DeleteStopPointCommand request, CancellationToken cancellationToken)
    {
        var deletedRows = await _dbContext.StopPoints
            .Where(w => w.Id == request.Id)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.IsDeleted, true), cancellationToken);

        if (deletedRows == 0)
        {
            throw new NotFoundException(nameof(StopPoint), request.Id);
        }
    }
}
