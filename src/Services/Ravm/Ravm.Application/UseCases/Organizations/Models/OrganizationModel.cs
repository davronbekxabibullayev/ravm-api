namespace Ravm.Application.UseCases.Organizations.Models;

using Ravm.Application.Common.Models;
using Ravm.Application.UseCases.OrganizationAddresses.Models;
using Ravm.Application.UseCases.OrganizationContacts.Models;

public class OrganizationModel : LocalizableName
{
    public Guid Id { get; set; }
    public required string Code { get; set; }
    public required string Tin { get; set; }
    public string? Okonx { get; set; }
    public string? Oked { get; set; }
    public bool IsDeleted { get; set; }
    public Guid? ParentId { get; set; }
    public OrganizationModel? Parent { get; set; }
    public ICollection<OrganizationAddressModel> OrganizationAddresses { get; set; } = Array.Empty<OrganizationAddressModel>();
    public ICollection<OrganizationContactModel> OrganizationContacts { get; set; } = Array.Empty<OrganizationContactModel>();
}




