namespace Ravm.Application.UseCases.WaybillFuels.Mappers;

using Ravm.Application.UseCases.WaybillFuels.Commands;
using Ravm.Application.UseCases.WaybillFuels.Models;

public class WaybillFuelMappingProfile : Profile
{
    public WaybillFuelMappingProfile()
    {
        CreateMap<WaybillFuel, WaybillFuelModel>();
        CreateMap<UpdateWaybillFuelCommand, WaybillFuel>();
    }
}
