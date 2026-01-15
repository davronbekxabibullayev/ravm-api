namespace Ravm.Application.UseCases.WaybillDetails.Validators;

using Ravm.Application.UseCases.WaybillDetails.Commands;

public class CreateWaybillDetailCommandValidator : AbstractValidator<CreateWaybillDetailCommand>
{
    public CreateWaybillDetailCommandValidator()
    {
        RuleFor(x => x.Date).NotEmpty();
        RuleFor(x => x.WaybillId).NotEmpty();
    }
}
