namespace Ravm.Application.UseCases.VehicleMarks.Mappers;

using Ravm.Application.UseCases.VehicleMarks.Commands;
using Ravm.Application.UseCases.VehicleMarks.Models;

public class VehicleMarkMappingProfile : Profile
{
    public VehicleMarkMappingProfile()
    {
        CreateMap<VehicleMark, VehicleMarkModel>();
        CreateMap<CreateVehicleMarkCommand, VehicleMark>();
        CreateMap<UpdateVehicleMarkCommand, VehicleMark>();
    }
}
