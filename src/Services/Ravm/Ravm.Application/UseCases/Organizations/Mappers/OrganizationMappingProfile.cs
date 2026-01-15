namespace Ravm.Application.UseCases.Organizations.Mappers;

using Ravm.Application.UseCases.Organizations.Commands;
using Ravm.Application.UseCases.Organizations.Models;

public class OrganizationMappingProfile : Profile
{
    public OrganizationMappingProfile()
    {
        CreateMap<Organization, OrganizationModel>();
        CreateMap<UpdateOrganizationCommand, Organization>();
        CreateMap<CreateOrganizationCommand, Organization>();
    }
}
