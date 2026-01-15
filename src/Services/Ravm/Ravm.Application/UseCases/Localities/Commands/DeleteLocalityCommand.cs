namespace Ravm.Application.UseCases.Localities.Commands;

using Microsoft.EntityFrameworkCore;

public record DeleteLocalityCommand(Guid Id) : IRequest;

internal class DeleteLocalityCommandHander(IAppDbContext dbContext) : IRequestHandler<DeleteLocalityCommand>
{
    private readonly IAppDbContext _dbContext = dbContext;
    public async Task Handle(DeleteLocalityCommand request, CancellationToken cancellationToken)
    {
        var deletedItems = await _dbContext.Localities
            .Where(x => x.Id == request.Id)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.IsDeleted, true), cancellationToken);

        if (deletedItems == 0)
        {
            throw new NotFoundException(nameof(Locality), request.Id);
        }
    }
}

