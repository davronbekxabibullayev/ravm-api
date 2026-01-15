namespace Ravm.Application.UseCases.Cities.Mappers;

using Ravm.Application.UseCases.Cities.Commands;
using Ravm.Application.UseCases.Cities.Models;

internal class CityMappingProfile : Profile
{
    public CityMappingProfile()
    {
        CreateMap<City, CityModel>();
        CreateMap<UpdateCityCommand, City>();
    }
}
