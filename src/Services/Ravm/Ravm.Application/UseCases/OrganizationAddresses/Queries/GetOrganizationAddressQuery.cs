namespace Ravm.Application.UseCases.OrganizationAddresses.Queries;
using Microsoft.EntityFrameworkCore;

public record GetOrganizationAddressQuery(Guid Id) : IRequest<OrganizationAddress>;

public sealed class GetOrganizationAddressQueryHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<GetOrganizationAddressQuery, OrganizationAddress>
{
    public async Task<OrganizationAddress> Handle(GetOrganizationAddressQuery query, CancellationToken cancellationToken)
    {
        var organizationAddress = await dbContext.OrganizationAddresses
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Organization), query.Id);

        return mapper.Map<OrganizationAddress>(organizationAddress);
    }
}
