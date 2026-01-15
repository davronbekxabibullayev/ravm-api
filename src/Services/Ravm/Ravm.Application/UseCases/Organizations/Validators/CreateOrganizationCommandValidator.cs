namespace Ravm.Application.UseCases.Organizations.Validators;
using Ravm.Application.UseCases.Organizations.Commands;

public class CreateOrganizationCommandValidator : AbstractValidator<CreateOrganizationCommand>
{
    public CreateOrganizationCommandValidator()
    {
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.Tin).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.NameRu).NotEmpty();

        RuleForEach(x => x.OrganizationAddresses).SetValidator(new CreateOrganizationAddressesValidator());
        RuleForEach(x => x.OrganizationContacts).SetValidator(new CreateOrganizationContactsValidator());
    }
}

public class CreateOrganizationAddressesValidator : AbstractValidator<CreateOrganizationAddressModel>
{
    public CreateOrganizationAddressesValidator()
    {
        RuleFor(x => x.AddressLine1).NotEmpty();
        RuleFor(x => x.CityId).NotEmpty();
        RuleFor(x => x.RegionId).NotEmpty();
    }
}

public class CreateOrganizationContactsValidator : AbstractValidator<CreateOrganizationContactModel>
{
    public CreateOrganizationContactsValidator()
    {
        RuleFor(x => x.FullName).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
    }
}
