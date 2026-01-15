namespace Ravm.Application.UseCases.WaybillFuels.Validators;

using Ravm.Application.UseCases.WaybillFuels.Commands;

public class CreateWaybillFuelCommandValidator : AbstractValidator<CreateWaybillFuelCommand>
{
    public CreateWaybillFuelCommandValidator()
    {
        RuleFor(x => x.FuelMark).NotEmpty();
        RuleFor(x => x.RefuellerFullName).NotEmpty();
        RuleFor(x => x.WaybillId).NotEmpty();
        RuleFor(x => x.WaybillDetailId).NotEmpty();
        RuleFor(x => x.FundingSource).IsInEnum();
        RuleFor(x => x.FuelType).IsInEnum();
    }
}
