namespace Ravm.Application.UseCases.Occupations.Commands;
using Microsoft.EntityFrameworkCore;

public record DeleteOccupationCommand(Guid Id) : IRequest;

internal class DeleteOccupationCommandHander(IAppDbContext dbContext) : IRequestHandler<DeleteOccupationCommand>
{
    private readonly IAppDbContext _dbContext = dbContext;
    public async Task Handle(DeleteOccupationCommand request, CancellationToken cancellationToken)
    {
        var deletedItems = await _dbContext.Occupations
            .Where(x => x.Id == request.Id)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.IsDeleted, true), cancellationToken);

        if (deletedItems == 0)
        {
            throw new NotFoundException(nameof(Domain.Models.Occupation), request.Id);
        }
    }
}
