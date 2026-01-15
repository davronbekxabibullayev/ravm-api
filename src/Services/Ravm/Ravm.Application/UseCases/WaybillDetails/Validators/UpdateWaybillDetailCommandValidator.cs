namespace Ravm.Application.UseCases.WaybillDetails.Validators;

using Ravm.Application.UseCases.WaybillDetails.Commands;

public class UpdateWaybillDetailCommandValidator : AbstractValidator<UpdateWaybillDetailCommand>
{
    public UpdateWaybillDetailCommandValidator()
    {
        RuleFor(x => x.Date).NotEmpty();
        RuleFor(x => x.WaybillId).NotEmpty();
    }
}
