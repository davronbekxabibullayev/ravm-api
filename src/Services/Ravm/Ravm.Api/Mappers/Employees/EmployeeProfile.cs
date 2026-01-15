namespace Ravm.Api.Mappers.Employees;

using AutoMapper;
using Ravm.Api.Models.Employees;
using Ravm.Application.UseCases.Employees.Commands;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<UpdateEmployeeRequest, UpdateEmployeeCommand>();
    }
}
