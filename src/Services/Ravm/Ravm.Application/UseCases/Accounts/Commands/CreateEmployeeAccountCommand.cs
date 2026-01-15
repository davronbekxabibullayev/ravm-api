namespace Ravm.Application.UseCases.Accounts.Commands;

using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public record CreateEmployeeAccountCommand(
    string UserName,
    string Password,
    string ConfirmPassword,
    string Email,
    Guid EmployeeId,
    string? PhoneNumber,
    string? Avatar) : IRequest<Guid>
{
    public IList<string>? Roles { get; init; }
};

internal class CreateEmployeeAccountCommandHandler(IMapper mapper, UserManager<User> userManager, IAppDbContext dbContext) : IRequestHandler<CreateEmployeeAccountCommand, Guid>
{
    public async Task<Guid> Handle(CreateEmployeeAccountCommand command, CancellationToken cancellationToken)
    {
        var normalizedUserName = userManager.NormalizeName(command.UserName);
        var normalizedEmail = userManager.NormalizeEmail(command.Email);

        var hasUserName = await userManager.Users
            .IgnoreQueryFilters()
            .AnyAsync(a => a.NormalizedUserName == normalizedUserName, cancellationToken);

        if (hasUserName)
        {
            throw new AlreadyExistsException(nameof(User), command.UserName);
        }

        var hasEmail = await userManager.Users
            .IgnoreQueryFilters()
            .AnyAsync(a => a.NormalizedEmail == normalizedEmail, cancellationToken);

        if (hasEmail)
        {
            throw new AlreadyExistsException(nameof(User), command.Email);
        }

        var user = mapper.Map<User>(command);

        var employee = await dbContext.Employees.FirstOrDefaultAsync(a => a.Id == command.EmployeeId, cancellationToken)
            ?? throw new NotFoundException(nameof(Employee), command.EmployeeId);

        user.OrganizationId = employee.OrganizationId;

        if (command.Password != command.ConfirmPassword)
        {
            throw new AccessDeniedException();
        }

        var createResult = await userManager.CreateAsync(user, command.Password);

        if (createResult.Succeeded)
        {
            await SetUserIdToEmployee(user.Id, command.EmployeeId);

            if (command.Roles != null)
                await userManager.AddToRolesAsync(user, command.Roles);

            return user.Id;
        }
        else
        {
            throw new AccessDeniedException();
        }
    }

    private async Task SetUserIdToEmployee(Guid userId, Guid employeeId)
    {
        await dbContext.Employees
            .Where(x => x.Id == employeeId)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.UserId, userId));
    }
}
