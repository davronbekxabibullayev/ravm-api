namespace Ravm.Application.UseCases.Countries.Commands;

using Microsoft.EntityFrameworkCore;

public record DeleteCountryCommand(Guid Id) : IRequest;

internal class DeleteCountryCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteCountryCommand>
{
    public async Task Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
    {
        var deletedRows = await dbContext.Countries
            .Where(w => w.Id == request.Id)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.IsDeleted, true), cancellationToken);

        if (deletedRows == 0)
        {
            throw new NotFoundException(nameof(Country), request.Id);
        }
    }
}
