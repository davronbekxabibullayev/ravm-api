namespace Ravm.Application.UseCases.RouteClassifications.Mappers;

using Ravm.Application.UseCases.RouteClassifications.Models;

internal class RoleMappingProfile : Profile
{
    public RoleMappingProfile()
    {
        CreateMap<Role, RoleModel>();
    }
}
