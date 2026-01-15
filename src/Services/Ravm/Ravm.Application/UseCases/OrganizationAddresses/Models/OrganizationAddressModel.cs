namespace Ravm.Application.UseCases.OrganizationAddresses.Models;
using Ravm.Domain.Enums;

public record OrganizationAddressModel
{
    public Guid Id { get; set; }

    public required string AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public AddressType Type { get; set; }

    public Guid CityId { get; set; }

    public Guid RegionId { get; set; }

    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public Guid OrganizationId { get; set; }

}
