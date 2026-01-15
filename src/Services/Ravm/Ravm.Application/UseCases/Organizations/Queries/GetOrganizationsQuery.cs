namespace Ravm.Application.UseCases.Organizations.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.Common;
using Ravm.Application.UseCases.Organizations.Models;
using Ravm.Application.Extensions;

public record GetOrganizationsQuery(Guid? ParentId, string? Name) : FilteringRequest, IRequest<PagedList<OrganizationModel>>;

internal sealed class GetOrganizationsQueryHandler(IAppDbContext dbContext,ICurrentUser currentUser, IMapper mapper) : IRequestHandler<GetOrganizationsQuery, PagedList<OrganizationModel>>
{
    public async Task<PagedList<OrganizationModel>> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken)
    {
        var organizations = dbContext.Organizations
            .Include(x => x.OrganizationAddresses)
            .Include(x => x.OrganizationContacts)
            .AsQueryable();

        if (!currentUser.IsAdmin)
        {
            organizations = dbContext.Organizations
                                     .IncludeChilds(currentUser.OrganizationId)
                                     .Include(x => x.OrganizationAddresses)
                                     .Include(x => x.OrganizationContacts)
                                     .AsQueryable();
        }

        if (request.ParentId.HasValue)
        {
            organizations = organizations.Where(w => w.ParentId == request.ParentId);
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            var organizationName = request.Name.ToLowerInvariant();

            organizations = organizations.Where(x => EF.Functions.ILike(x.Name, $"%{organizationName}%")
                                            || EF.Functions.ILike(x.NameRu, $"%{organizationName}%")
                                            || EF.Functions.ILike(x.NameUz!, $"%{organizationName}%")
                                            || EF.Functions.ILike(x.NameKa!, $"%{organizationName}%"));
        }
        return await organizations.ToPagedListAsync<Organization, OrganizationModel>(request, mapper);
    }
}
