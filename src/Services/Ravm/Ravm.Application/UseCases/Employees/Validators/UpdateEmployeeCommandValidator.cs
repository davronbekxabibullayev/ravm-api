namespace Ravm.Application.UseCases.Employees.Validators;

using Ravm.Application.UseCases.Employees.Commands;

public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Gender).NotEmpty();
        RuleFor(x => x.OrganizationId).NotEmpty();
        RuleFor(x => x.StaffNumber).NotEmpty();
        RuleFor(x => x.OccupationGroupType).IsInEnum();
    }
}
