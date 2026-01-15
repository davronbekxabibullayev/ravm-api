namespace Ravm.Application.UseCases.Organizations.Commands;

using Microsoft.EntityFrameworkCore;
using Ravm.Domain.Enums;
using Ravm.Domain.Exceptions;

public record CreateOrganizationCommand(
    string Name,
    string NameRu,
    string? NameUz,
    string? NameKa,
    string Code,
    string Tin,
    string? Okonx,
    string? Oked,
    Guid? ParentId) : IRequest
{
    public IEnumerable<CreateOrganizationAddressModel> OrganizationAddresses { get; init; } = new HashSet<CreateOrganizationAddressModel>();
    public IEnumerable<CreateOrganizationContactModel> OrganizationContacts { get; init; } = new HashSet<CreateOrganizationContactModel>();
}

public class CreateOrganizationCommandHandler(IAppDbContext dbContext) : IRequestHandler<CreateOrganizationCommand>
{
    public async Task Handle(CreateOrganizationCommand command, CancellationToken cancellationToken)
    {
        if (await IsExist(command))
        {
            throw new AlreadyExistsException(nameof(Organization), command.Code);
        }

        var organization = CreateOrganization(command);
        await dbContext.Organizations.AddAsync(organization, cancellationToken);
        AddAddresses(command, organization);
        AddContacts(command, organization);
        await dbContext.SaveChangesAsync(cancellationToken);

    }

    private static Organization CreateOrganization(CreateOrganizationCommand command)
    {
        return new Organization
        {
            Name = command.Name,
            NameUz = command.NameUz,
            NameKa = command.NameKa,
            NameRu = command.NameRu,
            Code = command.Code,
            Tin = command.Tin,
            Okonx = command.Okonx,
            Oked = command.Oked,
            ParentId = command.ParentId,
        };
    }

    private static void AddAddresses(CreateOrganizationCommand command, Organization organization)
    {
        foreach (var organizationAddress in command.OrganizationAddresses)
        {
            organization.OrganizationAddresses.Add(new OrganizationAddress
            {
                AddressLine1 = organizationAddress.AddressLine1,
                AddressLine2 = organizationAddress.AddressLine2,
                CityId = organizationAddress.CityId,
                RegionId = organizationAddress.RegionId,
                Latitude = organizationAddress.Latitude,
                Longitude = organizationAddress.Longitude,
                OrganizationId = organization.Id,
                Type = organizationAddress.Type
            });
        }
    }

    private static void AddContacts(CreateOrganizationCommand command, Organization organization)
    {
        foreach (var contact in command.OrganizationContacts)
        {
            organization.OrganizationContacts.Add(new OrganizationContact
            {
                Email = contact.Email,
                FullName = contact.FullName,
                OrganizationId = organization.Id,
                PhoneNumber = contact.PhoneNumber
            });
        }
    }
    private Task<bool> IsExist(CreateOrganizationCommand command)
    {
        return dbContext.Organizations.AnyAsync(x => x.Code == command.Code);
    }
}

public record CreateOrganizationAddressModel(
    string AddressLine1,
    string? AddressLine2,
    AddressType Type,
    Guid CityId,
    Guid RegionId,
    double Longitude,
    double Latitude);
public record CreateOrganizationContactModel(
    string FullName,
    string PhoneNumber,
    string? Email);
