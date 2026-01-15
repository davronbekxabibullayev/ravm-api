namespace Ravm.Application.UseCases.Accounts.Commands;

using MediatR;
using Microsoft.AspNetCore.Identity;

public record UpdateEmployeeAccountCommand(
    Guid Id,
    string Email,
    string? PhoneNumber,
    string? Avatar) : IRequest<Guid>
{
    public IList<string>? Roles { get; init; }
};

internal class UpdateEmployeeAccountCommandHandler(UserManager<User> userManager) : IRequestHandler<UpdateEmployeeAccountCommand, Guid>
{
    public async Task<Guid> Handle(UpdateEmployeeAccountCommand command, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(command.Id.ToString())
            ?? throw new NotFoundException(nameof(User), command.Id);

        var normalizedEmail = userManager.NormalizeEmail(command.Email);

        user.NormalizedEmail = normalizedEmail;
        user.Email = command.Email;
        user.PhoneNumber = command.PhoneNumber;
        user.Avatar = command.Avatar;

        var result = await userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            var oldRoles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, oldRoles);

            if (command.Roles != null)
                await userManager.AddToRolesAsync(user, command.Roles);
        }

        return user.Id;
    }
}
