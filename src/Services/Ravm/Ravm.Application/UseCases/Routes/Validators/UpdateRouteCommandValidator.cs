namespace Ravm.Application.UseCases.Routes.Validators;

using Ravm.Application.UseCases.Routes.Commands;

public class UpdateRouteCommandValidator : AbstractValidator<UpdateRouteCommand>
{
    public UpdateRouteCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.NameRu).NotEmpty();
        RuleFor(x => x.RouteClassificationId).NotEmpty();
        RuleFor(x => x.OrganizationId).NotEmpty();
    }
}
