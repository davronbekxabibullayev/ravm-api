namespace Ravm.Application.UseCases.Occupations.Validators;

using Ravm.Application.UseCases.Occupations.Commands;

public class CreateOccupationCommandValidator : AbstractValidator<CreateOccupationCommand>
{
    public CreateOccupationCommandValidator()
    {
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.NameRu).NotEmpty();
    }
}
