namespace Ravm.Application.UseCases.OrganizationAddresses.Validators;

using Ravm.Application.UseCases.OrganizationAddresses.Commands;

public class CreateOrganizationAddressCommandValidator : AbstractValidator<CreateOrganizationAddressCommand>
{
    public CreateOrganizationAddressCommandValidator()
    {
        RuleFor(x => x.AddressLine1).NotEmpty();
        RuleFor(x => x.CityId).NotEmpty();
        RuleFor(x => x.RegionId).NotEmpty();
    }
}
