namespace Ravm.Application.UseCases.Countries.Mappers;

using Ravm.Application.UseCases.Countries.Commands;
using Ravm.Application.UseCases.Countries.Models;

internal class CountryMappingProfile : Profile
{
    public CountryMappingProfile()
    {
        CreateMap<Country, CountryModel>();
        CreateMap<UpdateCountryCommand, Country>();
        CreateMap<CreateCountryCommand, Country>();
    }
}
