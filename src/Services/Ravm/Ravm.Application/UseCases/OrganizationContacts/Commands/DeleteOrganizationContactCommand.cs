namespace Ravm.Application.UseCases.OrganizationContacts.Commands;
using Microsoft.EntityFrameworkCore;

public record DeleteOrganizationContactCommand(Guid Id) : IRequest;

internal class DeleteOrganizationContactsCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteOrganizationContactCommand>
{
    public async Task Handle(DeleteOrganizationContactCommand request, CancellationToken cancellationToken)
    {
        var deletedRows = await dbContext.OrganizationContacts
            .Where(w => w.Id == request.Id)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.IsDeleted, true), cancellationToken);

        if (deletedRows == 0)
        {
            throw new NotFoundException(nameof(OrganizationContact), request.Id);
        }
    }
}
