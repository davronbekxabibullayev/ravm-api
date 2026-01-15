namespace Ravm.Application.UseCases.OrganizationContacts.Models;

public class OrganizationContactModel
{
    public Guid Id { get; set; }

    public required string FullName { get; set; }

    public required string PhoneNumber { get; set; }

    public string? Email { get; set; }

    public Guid OrganizationId { get; set; }
}
