namespace Ravm.Application.UseCases.Reasons.Validators;

using Ravm.Application.UseCases.Reasons.Commands;

public class CreateReasonCommandValidator : AbstractValidator<CreateReasonCommand>
{
    public CreateReasonCommandValidator()
    {
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.NameRu).NotEmpty();
    }
}
