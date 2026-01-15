namespace Ravm.Api.Mappers.Organizations;

using AutoMapper;
using Ravm.Api.Models.Organizations;
using Ravm.Application.UseCases.Organizations.Commands;

public class OrganizationProfile : Profile
{
    public OrganizationProfile()
    {
        CreateMap<UpdateOrganizationAddressRequest, UpdateOrganizationAddressModel>();
        CreateMap<UpdateOrganizationContactRequest, UpdateOrganizationContactModel>();
        CreateMap<UpdateOrganizationRequest, UpdateOrganizationCommand>();
    }
}
