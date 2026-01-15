namespace Ravm.Application.UseCases.VehicleModels.Validators;

using Ravm.Application.UseCases.VehicleModels.Commands;

public class CreateVehicleModelCommandValidator : AbstractValidator<CreateVehicleModelCommand>
{
    public CreateVehicleModelCommandValidator()
    {
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.VehicleMarkId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.NameRu).NotEmpty();
    }
}
