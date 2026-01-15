namespace Ravm.Application.UseCases.StopPoints.Mappers;

using Ravm.Application.UseCases.StopPoints.Commands;
using Ravm.Application.UseCases.StopPoints.Models;

public class StopPointMappingProfile : Profile
{
    public StopPointMappingProfile()
    {
        CreateMap<StopPoint, StopPointModel>();
        CreateMap<UpdateStopPointCommand, StopPoint>();
        CreateMap<CreateStopPointCommand, StopPoint>();
    }
}
