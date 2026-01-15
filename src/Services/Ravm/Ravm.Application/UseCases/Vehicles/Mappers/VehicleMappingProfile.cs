namespace Ravm.Application.UseCases.Vehicles.Mappers;

using Ravm.Application.UseCases.Vehicles.Commands;
using Ravm.Application.UseCases.Vehicles.Models;
public class VehicleMappingProfile : Profile
{
    public VehicleMappingProfile()
    {
        CreateMap<Vehicle, VehicleItem>();
        CreateMap<Vehicle, VehicleItemDto>();
        CreateMap<CreateVehicleCommand, Vehicle>();
        CreateMap<UpdateVehicleCommand, Vehicle>();
    }
}
