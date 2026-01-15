namespace Ravm.Application.UseCases.Localities.Mappers;

using Ravm.Application.UseCases.Localities.Commands;
using Ravm.Application.UseCases.Localities.Models;

internal class LocalityMapperProfile : Profile
{
    public LocalityMapperProfile()
    {
        CreateMap<Locality, LocalityModel>();
        CreateMap<UpdateLocalityCommand, Locality>();
        CreateMap<CreateLocalityCommand, Locality>();
    }
}
