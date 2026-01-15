namespace Ravm.Application.UseCases.Organizations.Commands;

using Microsoft.EntityFrameworkCore;

public record DeleteOrganizationCommand(Guid Id) : IRequest;

internal class DeleteOrganizationCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteOrganizationCommand>
{
    public async Task Handle(DeleteOrganizationCommand request, CancellationToken cancellationToken)
    {
        var organization = await dbContext.Organizations
            .Where(x => x.Id == request.Id)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.IsDeleted, true), cancellationToken);

        if (organization == 0)
        {
            throw new NotFoundException(nameof(Organization), request.Id);
        }

    }
}
