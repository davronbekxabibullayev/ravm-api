namespace Ravm.Application.UseCases.Localities.Validators;

using Ravm.Application.UseCases.Localities.Commands;

public class UpdateLocalityCommandValidator : AbstractValidator<UpdateLocalityCommand>
{
    public UpdateLocalityCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.NameRu).NotEmpty();
        RuleFor(x => x.Code).NotEmpty();
    }
}
