namespace Ravm.Application.UseCases.Employees.Mappers;

using Ravm.Application.UseCases.Accounts.Models;
using Ravm.Application.UseCases.Employees.Commands;
using Ravm.Application.UseCases.Employees.Models;

public class EmployeeMappingProfile : Profile
{
    public EmployeeMappingProfile()
    {
        CreateMap<CreateEmployeeCommand, Employee>()
            .ForMember(x => x.EmployeeOccupations, y => y.Ignore())
            .ForMember(x => x.EmployeeSpecializations, y => y.Ignore())
            .ForMember(x => x.FullName, y => y.MapFrom(a => $"{a.FirstName}"
                                                            + $" {a.LastName}"
                                                            + (string.IsNullOrWhiteSpace(a.MiddleName) ? string.Empty : $" {a.MiddleName}")))
            .ForMember(x => x.BirthDay, y => y.MapFrom(a => a.BirthDate.HasValue ? (object)a.BirthDate.Value.Day : null))
            .ForMember(x => x.BirthMonth, y => y.MapFrom(a => a.BirthDate.HasValue ? (object)a.BirthDate.Value.Month : null))
            .ForMember(x => x.BirthYear, y => y.MapFrom(a => a.BirthDate.HasValue ? (object)a.BirthDate.Value.Year : null));

        CreateMap<Employee, EmployeeModel>()
            .ForMember(x => x.Specializations, act => act.MapFrom(a => a.EmployeeSpecializations.Select(x => x.Specialization)))
            .ForMember(x => x.Occupations, act => act.MapFrom(a => a.EmployeeOccupations.Select(x => x.Occupation)));

        CreateMap<User, AccountModel>();

        CreateMap<UpdateEmployeeCommand, Employee>()
            .ForMember(x => x.EmployeeOccupations, y => y.Ignore())
            .ForMember(x => x.EmployeeSpecializations, y => y.Ignore())
            .ForMember(x => x.FullName, y => y.MapFrom(a => $"{a.FirstName}"
                                                            + $" {a.LastName}"
                                                            + (string.IsNullOrWhiteSpace(a.MiddleName) ? string.Empty : $" {a.MiddleName}")))
            .ForMember(x => x.BirthDay, y => y.MapFrom(a => a.BirthDate.HasValue ? (object)a.BirthDate.Value.Day : null))
            .ForMember(x => x.BirthMonth, y => y.MapFrom(a => a.BirthDate.HasValue ? (object)a.BirthDate.Value.Month : null))
            .ForMember(x => x.BirthYear, y => y.MapFrom(a => a.BirthDate.HasValue ? (object)a.BirthDate.Value.Year : null));
    }
}
