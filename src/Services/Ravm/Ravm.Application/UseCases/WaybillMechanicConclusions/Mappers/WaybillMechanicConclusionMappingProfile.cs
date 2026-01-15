namespace Ravm.Application.UseCases.WaybillMechanicConclusions.Mappers;

using Ravm.Application.UseCases.WaybillMechanicConclusions.Commands;
using Ravm.Application.UseCases.WaybillMechanicConclusions.Models;

public class WaybillMechanicConclusionMappingProfile : Profile
{
    public WaybillMechanicConclusionMappingProfile()
    {
        CreateMap<WaybillMechanicConclusion, WaybillMechanicConclusionModel>();
        CreateMap<WaybillMechanicConclusion, WaybillMechanicConclusionModel>()
            .ForMember(a => a.WaybillId, act => act.MapFrom(g => g.WaybillDetail!.WaybillId))
            .ForMember(a => a.Vehicle, act => act.MapFrom(b => b.WaybillDetail!.Waybill!.Vehicle));
        CreateMap<UpdateWaybillMechanicConclusionCommand, WaybillMechanicConclusion>();
    }
}
