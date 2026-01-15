namespace Ravm.Application.UseCases.WaybillTasks.Mappers;

using Ravm.Application.UseCases.WaybillTasks.Commands;
using Ravm.Application.UseCases.WaybillTasks.Models;

public class WaybillTaskMappingProfile : Profile
{
    public WaybillTaskMappingProfile()
    {
        CreateMap<WaybillTask, WaybillTaskModel>();
        CreateMap<UpdateWaybillTaskCommand, WaybillTask>();
    }
}
