namespace Ravm.Api.Models.Accounts;

using Ravm.Domain.Enums;

public class UpdateEmployeeByUserIdRequest
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }
    public Gender Gender { get; set; }
    public string? Pin { get; set; }
    public DateTimeOffset? BirthDate { get; set; }
}
