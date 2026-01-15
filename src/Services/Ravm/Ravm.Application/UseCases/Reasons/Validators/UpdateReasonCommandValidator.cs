namespace Ravm.Application.UseCases.Reasons.Validators;

using Ravm.Application.UseCases.Reasons.Commands;

public class UpdateReasonCommandValidator : AbstractValidator<UpdateReasonCommand>
{
    public UpdateReasonCommandValidator()
    {
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.NameRu).NotEmpty();
    }
}
