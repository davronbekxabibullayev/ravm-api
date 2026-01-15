namespace Ravm.Application.UseCases.OrganizationAddresses.Queries;

using Ravm.Application.Extensions;
using Ravm.Application.UseCases.OrganizationAddresses.Models;

public record GetOrganizationAddressesQuery(Guid? OrganizationId) : FilteringRequest, IRequest<PagedList<OrganizationAddressModel>>;

public sealed class GetOrganizationAddressesQueryHandler(IAppDbContext dbContext, IMapper mapper, ICurrentUser currentUser) : IRequestHandler<GetOrganizationAddressesQuery, PagedList<OrganizationAddressModel>>
{
    public async Task<PagedList<OrganizationAddressModel>> Handle(GetOrganizationAddressesQuery request, CancellationToken cancellationToken)
    {
        var organizationAddress = dbContext.OrganizationAddresses
                                           .IncludeChilds(currentUser.OrganizationId)
                                           .AsQueryable();

        if (request.OrganizationId.HasValue)
        {
            organizationAddress = organizationAddress.Where(x => x.OrganizationId == request.OrganizationId);
        }

        return await organizationAddress.ToPagedListAsync<OrganizationAddress, OrganizationAddressModel>(request, mapper);
    }
}
