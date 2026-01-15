namespace Ravm.Application.UseCases.VehicleModels.Mappers;

using Ravm.Application.UseCases.VehicleModels.Commands;
using Ravm.Application.UseCases.VehicleModels.Models;

public class VehicleModelMappingProfile : Profile
{
    public VehicleModelMappingProfile()
    {
        CreateMap<VehicleModel, VehicleModelModel>();
        CreateMap<CreateVehicleModelCommand, VehicleModel>();
        CreateMap<UpdateVehicleModelCommand, VehicleModel>();
    }
}
