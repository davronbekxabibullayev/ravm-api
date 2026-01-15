namespace Ravm.Api.Models.Employees;

using Ravm.Domain.Enums;

public record UpdateEmployeeRequest
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }
    public Gender Gender { get; set; }
    public DateTimeOffset? BirthDate { get; set; }
    public Guid OrganizationId { get; set; }
    public OccupationGroupType OccupationGroupType { get; set; }
    public required string StaffNumber { get; set; }
    public string? DriverLisenceNumber { get; set; }
    public string? Pin { get; set; }
    public ICollection<Guid> Occupations { get; set; } = Array.Empty<Guid>();
    public ICollection<Guid> Specializations { get; set; } = Array.Empty<Guid>();
}
