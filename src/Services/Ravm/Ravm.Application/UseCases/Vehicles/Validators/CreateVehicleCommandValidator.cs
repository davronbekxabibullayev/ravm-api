namespace Ravm.Application.UseCases.Vehicles.Validators;

using Ravm.Application.UseCases.Vehicles.Commands;

public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
{
    public CreateVehicleCommandValidator()
    {
        RuleFor(x => x.StateNumber).NotEmpty();
        RuleFor(x => x.OrganizationId).NotEmpty();
        RuleFor(x => x.VehicleModelId).NotEmpty();
        RuleFor(x => x.GarageNumber).NotEmpty();
    }
}
