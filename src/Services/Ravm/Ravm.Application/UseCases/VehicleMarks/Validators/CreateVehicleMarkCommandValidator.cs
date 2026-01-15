namespace Ravm.Application.UseCases.VehicleMarks.Validators;

using Ravm.Application.UseCases.VehicleMarks.Commands;

public class CreateVehicleMarkCommandValidator : AbstractValidator<CreateVehicleMarkCommand>
{
    public CreateVehicleMarkCommandValidator()
    {
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.NameRu).NotEmpty();
    }
}
