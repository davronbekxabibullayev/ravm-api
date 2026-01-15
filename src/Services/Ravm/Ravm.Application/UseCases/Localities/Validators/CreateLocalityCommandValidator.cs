namespace Ravm.Application.UseCases.Localities.Validators;

using Ravm.Application.UseCases.Localities.Commands;

public class CreateLocalityCommandValidator : AbstractValidator<CreateLocalityCommand>
{
    public CreateLocalityCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.NameRu).NotEmpty();
        RuleFor(x => x.Code).NotEmpty();
    }
}
