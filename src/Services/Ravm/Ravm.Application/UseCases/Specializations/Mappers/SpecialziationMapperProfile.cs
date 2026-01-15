namespace Ravm.Application.UseCases.Specializations.Mappers;

using Ravm.Application.UseCases.Specializations.Commands;
using Ravm.Application.UseCases.Specializations.Models;

public class SpecializationMapperProfile : Profile
{
    public SpecializationMapperProfile()
    {
        CreateMap<Specialization, SpecializationModel>();
        CreateMap<CreateSpecializationCommand, Specialization>();
        CreateMap<UpdateSpecializationCommand, Specialization>();
    }
}
