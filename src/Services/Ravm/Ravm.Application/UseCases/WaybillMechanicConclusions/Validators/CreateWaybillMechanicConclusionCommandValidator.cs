namespace Ravm.Application.UseCases.WaybillMechanicConclusions.Validators;

using Ravm.Application.UseCases.WaybillMechanicConclusions.Commands;

public class CreateWaybillMechanicConclusionCommandValidator
    : AbstractValidator<CreateWaybillMechanicConclusionCommand>
{
    public CreateWaybillMechanicConclusionCommandValidator()
    {
        RuleFor(x => x.WaybillDetailId).NotEmpty();
        RuleFor(x => x.MechanicConclusionType).IsInEnum();
        RuleFor(x => x.ReceivedDriverId)
            .NotEqual(Guid.Empty)
        .NotEmpty()
        .When(x => x.MechanicConclusionType == Domain.Enums.MechanicConclusionType.put);

        RuleFor(x => x.ReturnedDriverId)
            .NotEqual(Guid.Empty)
        .NotEmpty()
        .When(x => x.MechanicConclusionType != Domain.Enums.MechanicConclusionType.put);
    }
}
