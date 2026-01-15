namespace Ravm.Application.UseCases.Vehicles.Validators;

using Ravm.Application.UseCases.Vehicles.Commands;

public class UpdateVehicleCommandValidator : AbstractValidator<UpdateVehicleCommand>
{
    public UpdateVehicleCommandValidator()
    {
        RuleFor(x => x.StateNumber).NotEmpty();
        RuleFor(x => x.OrganizationId).NotEmpty();
        RuleFor(x => x.VehicleModelId).NotEmpty();
        RuleFor(x => x.GarageNumber).NotEmpty();
    }
}
