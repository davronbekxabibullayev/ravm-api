namespace Ravm.Application.UseCases.WaybillDoctorConclusions.Validators;

using Ravm.Application.UseCases.WaybillDoctorConclusions.Commands;

public class CreateWaybillDoctorConclusionCommandValidator
    : AbstractValidator<CreateWaybillDoctorConclusionCommand>
{
    public CreateWaybillDoctorConclusionCommandValidator()
    {
        RuleFor(x => x.WaybillDetailId).NotEmpty();
        RuleFor(x => x.WaybillDriverId).NotEmpty();
    }
}
