namespace Ravm.Application.UseCases.Waybills.Validators;

using Ravm.Application.UseCases.Waybills.Commands;

public class UpdateWaybillCommandValidator : AbstractValidator<UpdateWaybillCommand>
{
    public UpdateWaybillCommandValidator()
    {
        RuleFor(x => x.Number).NotEmpty();
        RuleFor(x => x.OrganizationId).NotEmpty();
        RuleFor(x => x.VehicleId).NotEmpty();
    }
}
