namespace Ravm.Application.UseCases.WaybillDrivers.Models;

using Ravm.Application.UseCases.Employees.Models;
using Ravm.Domain.Enums;

public class WaybillDriverModel
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid WaybillId { get; set; }
    public EmployeeModel? Employee { get; set; }
    public required string FullName { get; set; }
    public WaybillDriverRole? WaybillDriverRole { get; set; }
}
