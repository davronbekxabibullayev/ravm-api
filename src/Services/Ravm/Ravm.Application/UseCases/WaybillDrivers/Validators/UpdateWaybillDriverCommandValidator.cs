namespace Ravm.Application.UseCases.WaybillDrivers.Validators;

using Ravm.Application.UseCases.WaybillDrivers.Commands;

public class UpdateWaybillDriverCommandValidator : AbstractValidator<UpdateWaybillDriverCommand>
{
    public UpdateWaybillDriverCommandValidator()
    {
        RuleFor(x => x.EmployeeId).NotEmpty();
        RuleFor(x => x.WaybillId).NotEmpty();
        RuleFor(x => x.WaybillDriverRole).IsInEnum();
    }
}
