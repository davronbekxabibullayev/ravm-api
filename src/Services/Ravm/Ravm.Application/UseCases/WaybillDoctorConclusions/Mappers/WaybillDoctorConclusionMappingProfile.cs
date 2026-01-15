namespace Ravm.Application.UseCases.WaybillDoctorConclusions.Mappers;

using Ravm.Application.UseCases.WaybillDoctorConclusions.Commands;
using Ravm.Application.UseCases.WaybillDoctorConclusions.Models;

public class WaybillDoctorConclusionMappingProfile : Profile
{
    public WaybillDoctorConclusionMappingProfile()
    {
        CreateMap<WaybillDoctorConclusion, WaybillDoctorConclusionModel>()
            .ForMember(x => x.WaybillId, act => act.MapFrom(a => a.WaybillDetail!.WaybillId))
            .ForMember(x => x.WaybillDetailId, act => act.MapFrom(a => a.WaybillDetail!.Id));

        CreateMap<UpdateWaybillDoctorConclusionCommand, WaybillDoctorConclusion>();
    }
}
