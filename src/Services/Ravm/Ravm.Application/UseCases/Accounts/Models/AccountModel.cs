namespace Ravm.Application.UseCases.Accounts.Models;

public class AccountModel
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public Guid? EmployeeId { get; set; }
    public string? PhoneNumber { get; set; } = string.Empty;
    public string? Avatar { get; set; } = string.Empty;
    public IList<string>? Roles { get; set; }
}
