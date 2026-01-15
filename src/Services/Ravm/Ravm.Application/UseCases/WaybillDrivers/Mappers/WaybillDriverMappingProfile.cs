namespace Ravm.Application.UseCases.WaybillDrivers.Mappers;

using Ravm.Application.UseCases.WaybillDrivers.Commands;
using Ravm.Application.UseCases.WaybillDrivers.Models;

public class WaybillDriverMappingProfile : Profile
{
    public WaybillDriverMappingProfile()
    {
        CreateMap<WaybillDriver, WaybillDriverModel>()
            .ForMember(a => a.FullName, act => act.MapFrom(src => src.Employee!.FullName));
        CreateMap<UpdateWaybillDriverCommand, WaybillDriver>();
    }
}
