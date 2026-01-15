namespace Ravm.Application.UseCases.OrganizationContacts.Queries;

using Ravm.Application.UseCases.OrganizationContacts.Models;
using Ravm.Application.Extensions;

public record GetOrganizationContactsQuery(Guid? OrganizationId) : FilteringRequest, IRequest<PagedList<OrganizationContactModel>>;

public sealed class GetOrganizationContactsQueryHandler(IAppDbContext dbContext, IMapper mapper, ICurrentUser currentUser) : IRequestHandler<GetOrganizationContactsQuery, PagedList<OrganizationContactModel>>
{
    public async Task<PagedList<OrganizationContactModel>> Handle(GetOrganizationContactsQuery request, CancellationToken cancellationToken)
    {
        var organizationContacts = dbContext.OrganizationContacts
                                            .IncludeChilds(currentUser.OrganizationId).AsQueryable();

        if (request.OrganizationId.HasValue)
        {
            organizationContacts = organizationContacts.Where(x => x.OrganizationId == request.OrganizationId);
        }

        return await organizationContacts.ToPagedListAsync<OrganizationContact, OrganizationContactModel>(request, mapper);
    }
}
