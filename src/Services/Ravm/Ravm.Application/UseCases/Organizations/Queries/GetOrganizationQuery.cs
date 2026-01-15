namespace Ravm.Application.UseCases.Organizations.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.UseCases.Organizations.Models;

public record GetOrganizationQuery(Guid Id) : IRequest<OrganizationModel>;

internal sealed class GetOrganizationQueryHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<GetOrganizationQuery, OrganizationModel>
{
    public async Task<OrganizationModel> Handle(GetOrganizationQuery query, CancellationToken cancellationToken)
    {
        var organization = await dbContext.Organizations
            .Include(x => x.OrganizationAddresses)
            .Include(x => x.OrganizationContacts)
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Organization), query.Id);

        return mapper.Map<OrganizationModel>(organization);
    }
}
