namespace Ravm.Application.UseCases.Organizations.Commands;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Ravm.Domain.Enums;
using Ravm.Domain.Models;

public record UpdateOrganizationCommand(
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
    public Guid Id { get; set; }
    public IEnumerable<UpdateOrganizationAddressModel> OrganizationAddresses { get; init; } = new HashSet<UpdateOrganizationAddressModel>();
    public IEnumerable<UpdateOrganizationContactModel> OrganizationContacts { get; init; } = new HashSet<UpdateOrganizationContactModel>();
}

internal class UpdateOrganizationCommandHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateOrganizationCommand>
{
    private readonly IMapper _mapper = mapper;

    public async Task Handle(UpdateOrganizationCommand command, CancellationToken cancellationToken)
    {
        var organization = await GetOrganization(command)
            ?? throw new NotFoundException(nameof(Organization), command.Id);

        UpdateOrganization(command, organization);

        UpdateOrganizationAddress(command, organization);

        UpdateOrganizationContact(command, organization);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static void UpdateOrganization(UpdateOrganizationCommand command, Organization organization)
    {
        organization.Name = command.Name;
        organization.NameRu = command.NameRu;
        organization.NameUz = command.NameUz;
        organization.NameKa = command.NameKa;
        organization.Code = command.Code;
        organization.Tin = command.Tin;
        organization.Okonx = command.Okonx;
        organization.Oked = command.Oked;
        organization.ParentId = command.ParentId;
    }
    private void UpdateOrganizationAddress(UpdateOrganizationCommand command, Organization organization)
    {
        var incomingAddresses = command.OrganizationAddresses.Where(x => x.Id.HasValue).ToList();
        var organizationAddresses = organization.OrganizationAddresses;

        var creatableAdresses = command.OrganizationAddresses.Where(x => !x.Id.HasValue);
        var deletableAddresModels = organizationAddresses.Except(_mapper.Map<IEnumerable<OrganizationAddress>>(incomingAddresses), new OrganizationAddressComparer());

        foreach (var adress in deletableAddresModels)
        {
            adress.IsDeleted = true;
        }

        foreach (var address in creatableAdresses)
        {
            organization.OrganizationAddresses.Add(NewOrganizationAddress(address));
        }

        foreach (var address in incomingAddresses)
        {
            var organizationAddress = organizationAddresses.FirstOrDefault(x => x.Id == address.Id);

            if (organizationAddress != null)
            {
                UpdateOrganizationAddress(organizationAddress, address);
            }
        }
    }

    private void UpdateOrganizationContact(UpdateOrganizationCommand command, Organization organization)
    {
        var incomingContacts = command.OrganizationContacts.Where(x => x.Id.HasValue).ToList();
        var organizationContacts = organization.OrganizationContacts;

        var creatableContacts = command.OrganizationContacts.Where(x => !x.Id.HasValue);
        var deletableContacts = organizationContacts.Except(_mapper.Map<IEnumerable<OrganizationContact>>(incomingContacts), new OrganizationContactComparer());

        foreach (var contact in deletableContacts)
        {
            contact.IsDeleted = true;
        }

        foreach (var contact in creatableContacts)
        {
            organization.OrganizationContacts.Add(NewOrganizationContact(contact));
        }

        foreach (var contact in incomingContacts)
        {
            var organizationAddress = organizationContacts.FirstOrDefault(x => x.Id == contact.Id);

            if (organizationAddress != null)
            {
                UpdateOrganizationContact(organizationAddress, contact);
            }
        }
    }

    private async Task<Organization?> GetOrganization(UpdateOrganizationCommand command)
    {
        return await dbContext.Organizations
            .AsTracking()
            .Include(x => x.OrganizationAddresses)
            .Include(x => x.OrganizationContacts)
            .FirstOrDefaultAsync(x => x.Id == command.Id);
    }

    private static OrganizationAddress NewOrganizationAddress(UpdateOrganizationAddressModel model)
    {
        return new OrganizationAddress
        {
            AddressLine1 = model.AddressLine1,
            AddressLine2 = model.AddressLine2,
            Type = model.Type,
            CityId = model.CityId,
            RegionId = model.RegionId,
            Latitude = model.Latitude,
            Longitude = model.Longitude
        };
    }

    private static void UpdateOrganizationAddress(OrganizationAddress organizationAddress, UpdateOrganizationAddressModel model)
    {
        organizationAddress.AddressLine1 = model.AddressLine1;
        organizationAddress.AddressLine2 = model.AddressLine2;
        organizationAddress.Type = model.Type;
        organizationAddress.CityId = model.CityId;
        organizationAddress.RegionId = model.RegionId;
        organizationAddress.Latitude = model.Latitude;
        organizationAddress.Longitude = model.Longitude;
    }

    private static OrganizationContact NewOrganizationContact(UpdateOrganizationContactModel model)
    {
        return new OrganizationContact
        {
            FullName = model.FullName,
            PhoneNumber = model.PhoneNumber,
            Email = model.Email
        };
    }

    private static void UpdateOrganizationContact(OrganizationContact organizationContact, UpdateOrganizationContactModel model)
    {
        organizationContact.FullName = model.FullName;
        organizationContact.PhoneNumber = model.PhoneNumber;
        organizationContact.Email = model.Email;
    }
}


public record UpdateOrganizationAddressModel(
    Guid? Id,
    string AddressLine1,
    string? AddressLine2,
    AddressType Type,
    Guid CityId,
    Guid RegionId,
    double Longitude,
    double Latitude);

public record UpdateOrganizationContactModel(
    Guid? Id,
    string FullName,
    string PhoneNumber,
    string? Email);

public class OrganizationAddressComparer : IEqualityComparer<OrganizationAddress>
{
    public bool Equals(OrganizationAddress? x, OrganizationAddress? y)
    {
        return x?.Id == y?.Id;
    }

    public int GetHashCode([DisallowNull] OrganizationAddress obj)
    {
        return obj.Id.GetHashCode();
    }
}

public class OrganizationContactComparer : IEqualityComparer<OrganizationContact>
{
    public bool Equals(OrganizationContact? x, OrganizationContact? y)
    {
        return x?.Id == y?.Id;
    }

    public int GetHashCode([DisallowNull] OrganizationContact obj)
    {
        return obj.Id.GetHashCode();
    }
}
