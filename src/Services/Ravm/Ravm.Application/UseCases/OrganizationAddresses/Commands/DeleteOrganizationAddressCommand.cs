namespace Ravm.Application.UseCases.OrganizationAddresses.Commands;
using Microsoft.EntityFrameworkCore;

public record DeleteOrganizationAddressCommand(Guid Id) : IRequest;

internal class DeleteOrganizationAddressCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteOrganizationAddressCommand>
{
    public async Task Handle(DeleteOrganizationAddressCommand request, CancellationToken cancellationToken)
    {
        var deletedRows = await dbContext.OrganizationAddresses
            .Where(w => w.Id == request.Id)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.IsDeleted, true), cancellationToken);

        if (deletedRows == 0)
        {
            throw new NotFoundException(nameof(OrganizationAddress), request.Id);
        }
    }
}
