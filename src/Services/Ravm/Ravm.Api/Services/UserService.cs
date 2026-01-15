namespace Ravm.Api.Services;

using Microsoft.AspNetCore.Identity;
using Ravm.Api.Models.Accounts;
using Ravm.Domain.Exceptions;

public interface IUserService
{
    Task UpdateEmployeeAccountPassword(UpdateEmployeeAccountPasswordRequest request);
}

public class UserService(UserManager<User> userManager) : IUserService
{
    public async Task UpdateEmployeeAccountPassword(UpdateEmployeeAccountPasswordRequest request)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString())
           ?? throw new NotFoundException($"User with id {request.UserId} is not found!");

        if (request.Password != request.ConfirmPassword)
        {
            throw new AccessDeniedException();
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        await userManager.ResetPasswordAsync(user, token, request.Password);
    }
}
