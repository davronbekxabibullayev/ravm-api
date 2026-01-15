namespace Ravm.Application.UseCases.OrganizationAddresses.Commands;

using Ravm.Domain.Enums;

public record CreateOrganizationAddressCommand : IRequest
{
    public required string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public AddressType Type { get; set; }
    public Guid CityId { get; set; }
    public Guid RegionId { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public Guid OrganizationId { get; set; }
}

public class CreateOrganizationAddressCommandHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<CreateOrganizationAddressCommand>
{
    public async Task Handle(CreateOrganizationAddressCommand request, CancellationToken cancellationToken)
    {
        var organizationAddresses = mapper.Map<OrganizationAddress>(request);

        await dbContext.OrganizationAddresses.AddAsync(organizationAddresses, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
