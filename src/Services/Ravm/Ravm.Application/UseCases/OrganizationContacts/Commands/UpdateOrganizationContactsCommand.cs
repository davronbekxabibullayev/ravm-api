namespace Ravm.Application.UseCases.OrganizationContacts.Commands;
using Microsoft.EntityFrameworkCore;

public record UpdateOrganizationContactsCommand(
    Guid Id,
    string FullName,
    string PhoneNumber,
    string? Email) : IRequest;

internal class UpdateOrganizationContactsCommandHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateOrganizationContactsCommand>
{
    public async Task Handle(UpdateOrganizationContactsCommand request, CancellationToken cancellationToken)
    {
        var organizationContact = await GetOrganizationContactAsync(request.Id)
            ?? throw new NotFoundException(nameof(OrganizationAddress), request.Id);

        mapper.Map(request, organizationContact);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private Task<OrganizationContact?> GetOrganizationContactAsync(Guid id)
    {
        return dbContext.OrganizationContacts
                    .AsTracking()
                    .FirstOrDefaultAsync(w => w.Id == id);
    }
}
