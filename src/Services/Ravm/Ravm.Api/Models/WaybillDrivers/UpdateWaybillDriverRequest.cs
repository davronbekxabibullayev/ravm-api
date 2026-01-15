namespace Ravm.Api.Models.WaybillDrivers;

using Ravm.Domain.Enums;

public class UpdateWaybillDriverRequest
{
    public Guid EmployeeId { get; set; }
    public Guid WaybillId { get; set; }
    public WaybillDriverRole? WaybillDriverRole { get; set; }
}
