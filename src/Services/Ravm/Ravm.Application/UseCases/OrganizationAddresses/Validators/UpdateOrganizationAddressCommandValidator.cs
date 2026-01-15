namespace Ravm.Application.UseCases.OrganizationAddresses.Validators;

using Ravm.Application.UseCases.OrganizationAddresses.Commands;

public class UpdateOrganizationAddressCommandValidator : AbstractValidator<UpdateOrganizationAddressCommand>
{
    public UpdateOrganizationAddressCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.AddressLine1).NotEmpty();
        RuleFor(x => x.CityId).NotEmpty();
        RuleFor(x => x.RegionId).NotEmpty();
    }
}
