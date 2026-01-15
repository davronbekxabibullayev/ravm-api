namespace Ravm.Application.UseCases.Employees.Models;

using System;
using Ravm.Application.UseCases.Occupations.Models;
using Ravm.Application.UseCases.Specializations.Models;
using Ravm.Domain.Enums;

public class EmployeeModel
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }
    public required string FullName { get; set; }
    public Gender Gender { get; set; }
    public DateTimeOffset? BirthDate { get; set; }
    public int? BirthDay { get; set; }
    public int? BirthMonth { get; set; }
    public int? BirthYear { get; set; }
    public Guid OrganizationId { get; set; }
    public Guid? UserId { get; set; }
    public required string StaffNumber { get; set; }
    public string? DriverLisenceNumber { get; set; }
    public string? Pin { get; set; }
    public OccupationGroupType OccupationGroupType { get; set; }
    public ICollection<OccupationModel> Occupations { get; set; } = Array.Empty<OccupationModel>();
    public ICollection<SpecializationModel> Specializations { get; set; } = Array.Empty<SpecializationModel>();
}
