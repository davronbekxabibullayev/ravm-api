namespace Ravm.Application.UseCases.RouteClassifications.Commands;

using Microsoft.EntityFrameworkCore;

public record DeleteRouteClassificationCommand(Guid Id) : IRequest;

internal class DeleteRouteClassificationCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteRouteClassificationCommand>
{
    private readonly IAppDbContext _dbContext = dbContext;

    public async Task Handle(DeleteRouteClassificationCommand request, CancellationToken cancellationToken)
    {
        var deletedRows = await _dbContext.RouteClassifications
            .Where(e => e.Id == request.Id)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.IsDeleted, true), cancellationToken);

        if (deletedRows == 0)
        {
            throw new NotFoundException(nameof(RouteClassification), request.Id);
        }
    }
}


