namespace Ravm.Application.UseCases.Accounts.Validators;

using Ravm.Application.UseCases.Accounts.Commands;

public class UpdateEmployeeAccountCommandValidator : AbstractValidator<UpdateEmployeeAccountCommand>
{
    public UpdateEmployeeAccountCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
