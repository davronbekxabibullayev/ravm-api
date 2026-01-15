namespace Ravm.Application.UseCases.OrganizationContacts.Queries;
using Microsoft.EntityFrameworkCore;

public record GetOrganizationContactQuery(Guid Id) : IRequest<OrganizationContact>;

public sealed class GetOrganizationContactQueryHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<GetOrganizationContactQuery, OrganizationContact>
{
    public async Task<OrganizationContact> Handle(GetOrganizationContactQuery query, CancellationToken cancellationToken)
    {
        var organizationContact = await dbContext.OrganizationContacts
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Organization), query.Id);

        return mapper.Map<OrganizationContact>(organizationContact);
    }
}
