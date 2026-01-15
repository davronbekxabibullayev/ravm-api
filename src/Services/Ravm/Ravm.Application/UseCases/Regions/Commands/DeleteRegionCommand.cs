namespace Ravm.Application.UseCases.Regions.Commands;

using Microsoft.EntityFrameworkCore;

public record DeleteRegionCommand(Guid Id) : IRequest;


internal class DeleteRegionCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteRegionCommand>
{
    private readonly IAppDbContext _dbContext = dbContext;

    public async Task Handle(DeleteRegionCommand request, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Regions
            .Where(x => x.Id == request.Id)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.IsDeleted, true), cancellationToken);

        if (result == 0)
        {
            throw new NotFoundException(nameof(Region), request.Id);
        }
    }
}
