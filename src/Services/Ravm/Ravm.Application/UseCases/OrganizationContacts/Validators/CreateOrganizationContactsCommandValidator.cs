namespace Ravm.Application.UseCases.OrganizationContacts.Validators;

using Ravm.Application.UseCases.OrganizationContacts.Commands;

public class CreateOrganizationContactsCommandValidator : AbstractValidator<CreateOrganizationContactCommand>
{
    public CreateOrganizationContactsCommandValidator()
    {
        RuleFor(x => x.FullName).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
    }
}
