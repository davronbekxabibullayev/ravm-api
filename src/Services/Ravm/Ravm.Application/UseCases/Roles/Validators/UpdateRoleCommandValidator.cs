namespace Ravm.Application.UseCases.RouteClassifications.Validators;

using Ravm.Application.UseCases.Roles.Commands;

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommond>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
