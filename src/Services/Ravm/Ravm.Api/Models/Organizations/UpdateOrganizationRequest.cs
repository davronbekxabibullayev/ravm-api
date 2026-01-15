namespace Ravm.Api.Models.Organizations;

using Ravm.Domain.Enums;

public record UpdateOrganizationRequest
{
    public UpdateOrganizationRequest()
    {
        OrganizationAddresses = new HashSet<UpdateOrganizationAddressRequest>();
        OrganizationContacts = new HashSet<UpdateOrganizationContactRequest>();
    }
    public required string Name { get; set; }
    public required string NameRu { get; set; }
    public string? NameUz { get; set; }
    public string? NameKa { get; set; }
    public required string Code { get; set; }
    public required string Tin { get; set; }
    public string? Okonx { get; set; }
    public string? Oked { get; set; }
    public Guid? ParentId { get; set; }

    public IEnumerable<UpdateOrganizationAddressRequest> OrganizationAddresses { get; init; }

    public IEnumerable<UpdateOrganizationContactRequest> OrganizationContacts { get; init; }
}

public record UpdateOrganizationAddressRequest(
    Guid? Id,
    string AddressLine1,
    string? AddressLine2,
    AddressType Type,
    Guid CityId,
    Guid RegionId,
    double Longitude,
    double Latitude);

public record UpdateOrganizationContactRequest(
    Guid? Id,
    string FullName,
    string PhoneNumber,
    string? Email);
