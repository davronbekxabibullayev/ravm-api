namespace Ravm.Application.UseCases.OrganizationAddresses.Mappers;

using Ravm.Application.UseCases.OrganizationAddresses.Commands;
using Ravm.Application.UseCases.OrganizationAddresses.Models;
using Ravm.Application.UseCases.Organizations.Commands;

public class OrganizationAddressMappingProfile : Profile
{
    public OrganizationAddressMappingProfile()
    {
        CreateMap<OrganizationAddress, OrganizationAddressModel>();
        CreateMap<UpdateOrganizationAddressModel, OrganizationAddress>();
        CreateMap<CreateOrganizationAddressCommand, Organization>();
        CreateMap<UpdateOrganizationAddressCommand, Organization>();
    }
}
