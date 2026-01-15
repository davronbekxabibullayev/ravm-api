namespace Ravm.Application.UseCases.Occupations.Mappers;

using Ravm.Application.UseCases.Occupations.Commands;
using Ravm.Application.UseCases.Occupations.Models;

public class OccupationMappingProfile : Profile
{
    public OccupationMappingProfile()
    {
        CreateMap<Domain.Models.Occupation, OccupationModel>();
        CreateMap<CreateOccupationCommand, Domain.Models.Occupation>();
        CreateMap<UpdateOccupationCommand, Domain.Models.Occupation>();
    }
}
