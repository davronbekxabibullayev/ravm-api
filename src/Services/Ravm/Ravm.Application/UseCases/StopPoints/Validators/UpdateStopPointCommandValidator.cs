namespace Ravm.Application.UseCases.StopPoints.Validators;

using Ravm.Application.UseCases.StopPoints.Commands;

public class UpdateStopPointCommandValidator : AbstractValidator<UpdateStopPointCommand>
{
    public UpdateStopPointCommandValidator()
    {
        RuleFor(e => e.Code).NotEmpty().MaximumLength(32);
        RuleFor(e => e.Name).NotEmpty();
        RuleFor(e => e.NameRu).NotEmpty();
    }
}
