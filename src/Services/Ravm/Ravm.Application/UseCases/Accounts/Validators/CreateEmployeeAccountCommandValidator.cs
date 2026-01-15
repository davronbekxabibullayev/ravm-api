namespace Ravm.Application.UseCases.Accounts.Validators;

using Ravm.Application.UseCases.Accounts.Commands;

public class CreateEmployeeAccountCommandValidator : AbstractValidator<CreateEmployeeAccountCommand>
{
    public CreateEmployeeAccountCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.ConfirmPassword).NotEmpty();
        RuleFor(x => x.Password == x.ConfirmPassword);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(a => a.UserName)
            .Must(BeValidUsername)
            .WithMessage("Username may not contain any of the characters: !@#$%^&*()_=-?:;№`~[]{}|,/'");
    }

    public static bool BeValidUsername(string userName)
    {
        var invalidString = "!@#$%^&*()_=-?:;№`~[]{}|,/'";
        return !userName.Any(a => invalidString.Contains(a));
    }
}
