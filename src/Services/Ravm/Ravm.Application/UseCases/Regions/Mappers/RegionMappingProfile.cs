namespace Ravm.Application.UseCases.Regions.Mappers;

using Ravm.Application.UseCases.Regions.Commands;
using Ravm.Application.UseCases.Regions.Models;

public class RegionMappingProfile : Profile
{
    public RegionMappingProfile()
    {
        CreateMap<Region, RegionModel>();
        CreateMap<UpdateRegionCommand, Region>();
        CreateMap<CreateRegionCommand, Region>();
    }
}
