namespace Ravm.Application.UseCases.OrganizationContacts.Validators;

using Ravm.Application.UseCases.OrganizationContacts.Commands;

public class UpdateOrganizationContactsCommandValidator : AbstractValidator<UpdateOrganizationContactsCommand>
{
    public UpdateOrganizationContactsCommandValidator()
    {
        RuleFor(x => x.FullName).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
    }
}
