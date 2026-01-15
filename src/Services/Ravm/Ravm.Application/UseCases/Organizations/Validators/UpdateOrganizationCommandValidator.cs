namespace Ravm.Application.UseCases.Organizations.Validators;
using Ravm.Application.UseCases.Organizations.Commands;

public class UpdateOrganizationCommandValidator : AbstractValidator<UpdateOrganizationCommand>
{
    public UpdateOrganizationCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.NameRu).NotEmpty();
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.Tin).NotEmpty();

        RuleForEach(x => x.OrganizationAddresses).SetValidator(new UpdateOrganizationAddressesValidator());
        RuleForEach(x => x.OrganizationContacts).SetValidator(new UpdateOrganizationContactsValidator());
    }
}

public class UpdateOrganizationAddressesValidator : AbstractValidator<UpdateOrganizationAddressModel>
{
    public UpdateOrganizationAddressesValidator()
    {
        RuleFor(x => x.AddressLine1).NotEmpty();
        RuleFor(x => x.CityId).NotEmpty();
        RuleFor(x => x.RegionId).NotEmpty();
    }
}

public class UpdateOrganizationContactsValidator : AbstractValidator<UpdateOrganizationContactModel>
{
    public UpdateOrganizationContactsValidator()
    {
        RuleFor(x => x.FullName).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
    }
}
