namespace Ravm.Application.UseCases.OrganizationContacts.Commands;
public record CreateOrganizationContactCommand : IRequest
{
    public required string FullName { get; set; }

    public required string PhoneNumber { get; set; }

    public string? Email { get; set; }

    public Guid OrganizationId { get; set; }

}

public class CreateOrganizationContactsCommandHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<CreateOrganizationContactCommand>
{
    public async Task Handle(CreateOrganizationContactCommand request, CancellationToken cancellationToken)
    {
        var organizationContacts = mapper.Map<OrganizationContact>(request);

        await dbContext.OrganizationContacts.AddAsync(organizationContacts, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
