namespace Ravm.Application.UseCases.RouteClassifications.Validators;

using Ravm.Application.UseCases.Roles.Commands;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommond>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
