namespace Ravm.Application.UseCases.Employees.Validators;

using FluentValidation;
using Ravm.Application.UseCases.Employees.Commands;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.OrganizationId).NotEmpty();
        RuleFor(x => x.StaffNumber).NotEmpty();
        RuleFor(x => x.OccupationGroupType).IsInEnum();
    }
}
