namespace Ravm.Application.UseCases.OrganizationContacts.Mappers;

using Ravm.Application.UseCases.OrganizationContacts.Models;
using Ravm.Application.UseCases.Organizations.Commands;

public class OrganizationContactsMappingProfile : Profile
{
    public OrganizationContactsMappingProfile()
    {
        CreateMap<OrganizationContact, OrganizationContactModel>();
        CreateMap<UpdateOrganizationContactModel, OrganizationContact>();
    }
}
