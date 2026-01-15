namespace Ravm.Application.UseCases.RouteClassifications.Validators;

using Ravm.Application.UseCases.RouteClassifications.Commands;

public class CreateRouteClassificationCommandValidator : AbstractValidator<CreateRouteClassificationCommand>
{
    public CreateRouteClassificationCommandValidator()
    {
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.NameRu).NotEmpty();
    }

}
