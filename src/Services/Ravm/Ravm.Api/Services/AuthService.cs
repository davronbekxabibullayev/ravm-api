namespace Ravm.Api.Services;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ravm.Api.Models.Auth;
using Ravm.Api.Utils;
using Ravm.Application.Common;
using Ravm.Domain.Exceptions;
using Ravm.Domain.Models;

public interface IAuthService
{
    Task<IdentityResult> Register(RegisterRequest registration);
    Task<TokenResponse> Login(LoginRequest login);
}

public class AuthService(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    IUserStore<User> userStore,
    IAppDbContext dbContext) : IAuthService
{
    // Validate the email address using DataAnnotations like the UserValidator does when RequireUniqueEmail = true.
    private static readonly EmailAddressAttribute EmailAddressAttribute = new();



    public async Task<IdentityResult> Register(RegisterRequest registration)
    {
        if (!userManager.SupportsUserEmail)
        {
            throw new NotSupportedException($"{nameof(AuthService)} requires a user store with email support.");
        }

        var emailStore = (IUserEmailStore<User>)userStore;
        var email = registration.Email;
        var userName = registration.UserName;

        if (string.IsNullOrEmpty(email) || !EmailAddressAttribute.IsValid(email))
        {
            return IdentityResult.Failed(userManager.ErrorDescriber.InvalidEmail(email));
        }

        Employee? employee = null;
        if (registration.EmployeeId.HasValue)
        {
            employee = await dbContext.Employees
                                      .AsTracking()
                                      .FirstOrDefaultAsync(x => x.Id.Equals(registration.EmployeeId))
                                       ?? throw new NotFoundException(nameof(Employee), registration.EmployeeId);
        }

        var user = new User();
        await userStore.SetUserNameAsync(user, userName, CancellationToken.None);
        await emailStore.SetEmailAsync(user, email, CancellationToken.None);
        var result = await userManager.CreateAsync(user, registration.Password);

        if (result.Succeeded && employee != null)
        {
            user.OrganizationId = employee.OrganizationId;
            user.EmployeeId = registration.EmployeeId;
            employee.UserId = user.Id;

            await dbContext.SaveChangesAsync();
        }

        return result;
    }

    public async Task<TokenResponse> Login(LoginRequest login)
    {
        var user = await userManager.FindByNameAsync(login.UserName)
            ?? throw new AccessDeniedException("Username incorrect.");

        var result = await signInManager.PasswordSignInAsync(login.UserName, login.Password, isPersistent: false, lockoutOnFailure: true);

        if (!result.Succeeded)
        {
            throw new AccessDeniedException("Password incorrect.");
        }

        // Generate tokens

        var accessToken = await TokenUtils.GenerateAccessToken(user, "BE699EDC43FB42FC8D55303949BDAEEC", userManager);
        var refreshToken = TokenUtils.GenerateRefreshToken();

        return new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}
