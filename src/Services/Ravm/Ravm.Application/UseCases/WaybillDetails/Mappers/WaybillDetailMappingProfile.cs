namespace Ravm.Application.UseCases.WaybillDetails.Mappers;

using Ravm.Application.UseCases.WaybillDetails.Commands;
using Ravm.Application.UseCases.WaybillDetails.Models;

public class WaybillDetailMappingProfile : Profile
{
    public WaybillDetailMappingProfile()
    {
        CreateMap<WaybillDetail, WaybillDetailModel>();
        CreateMap<WaybillDetail, WaybillDetailModel>()
           .ForMember(x => x.Waybill, opt => opt.MapFrom(src => src.Waybill))
           .ForMember(x => x.Vehicle, opt => opt.MapFrom(src => src.Waybill!.Vehicle));

        CreateMap<CreateWaybillDetailCommand, WaybillDetail>();
        CreateMap<UpdateWaybillDetailCommand, WaybillDetail>();
    }
}
