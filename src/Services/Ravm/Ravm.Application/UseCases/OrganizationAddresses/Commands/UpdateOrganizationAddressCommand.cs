namespace Ravm.Application.UseCases.OrganizationAddresses.Commands;
using Microsoft.EntityFrameworkCore;
using Ravm.Domain.Enums;

public record UpdateOrganizationAddressCommand(
    Guid Id,
    string AddressLine1,
    string? AddressLine2,
    AddressType? Type,
    Guid CityId,
    Guid RegionId,
    string? Longitude,
    string? Latitude,
    Guid OrganizationId) : IRequest;

internal class UpdateOrganizationAddressCommandHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateOrganizationAddressCommand>
{
    public async Task Handle(UpdateOrganizationAddressCommand request, CancellationToken cancellationToken)
    {
        var organizationAddress = await GetOrganizationAddressAsync(request.Id)
            ?? throw new NotFoundException(nameof(OrganizationAddress), request.Id);

        mapper.Map(request, organizationAddress);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private Task<OrganizationAddress?> GetOrganizationAddressAsync(Guid id)
    {
        return dbContext.OrganizationAddresses
                    .AsTracking()
                    .FirstOrDefaultAsync(w => w.Id == id);
    }
}
