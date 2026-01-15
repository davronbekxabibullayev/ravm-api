namespace Ravm.Application.UseCases.Waybills.Mappers;

using Ravm.Application.UseCases.Employees.Models;
using Ravm.Application.UseCases.Waybills.Commands;
using Ravm.Application.UseCases.Waybills.Models;

public class WaybillMappingProfile : Profile
{
    public WaybillMappingProfile()
    {
        CreateMap<Employee, EmployeeModel>();
        CreateMap<Waybill, WaybillModel>()
            .ForMember(a => a.Drivers, act => act.MapFrom(b => b.WaybillDrivers.Select(g => g.Employee)));
        CreateMap<UpdateWaybillCommand, Waybill>();
    }
}
