namespace Ravm.Application.UseCases.WaybillDoctorConclusions.Validators;

using Ravm.Application.UseCases.WaybillDoctorConclusions.Commands;

public class UpdateWaybillDoctorConclusionCommandValidator
    : AbstractValidator<UpdateWaybillDoctorConclusionCommand>
{
    public UpdateWaybillDoctorConclusionCommandValidator()
    {
        RuleFor(x => x.WaybillDetailId).NotEmpty();
        RuleFor(x => x.WaybillDriverId).NotEmpty();
    }
}
