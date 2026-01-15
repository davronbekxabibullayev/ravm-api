namespace Ravm.Application.UseCases.Occupations.Validators;

using Ravm.Application.UseCases.Occupations.Commands;

public class UpdateOccupationCommandValidator : AbstractValidator<UpdateOccupationCommand>
{
    public UpdateOccupationCommandValidator()
    {
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.NameRu).NotEmpty();
    }
}
