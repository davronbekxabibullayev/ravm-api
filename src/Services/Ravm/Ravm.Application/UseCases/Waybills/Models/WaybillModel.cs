namespace Ravm.Application.UseCases.Waybills.Models;

using Ravm.Application.UseCases.Employees.Models;
using Ravm.Application.UseCases.Routes.Models;
using Ravm.Application.UseCases.Vehicles.Models;
using Ravm.Application.UseCases.WaybillDetails.Models;
using Ravm.Application.UseCases.WaybillFuels.Models;
using Ravm.Application.UseCases.WaybillTasks.Models;

public class WaybillModel
{
    public WaybillModel()
    {
        Fuels = new HashSet<WaybillFuelModel>();
        Tasks = new HashSet<WaybillTaskModel>();
        Details = new HashSet<WaybillDetailModel>();
        Drivers = new HashSet<EmployeeModel>();
    }
    public Guid Id { get; set; }
    public string Number { get; set; } = default!;
    public Guid OrganizationId { get; set; }
    public DateTimeOffset ExpireDate { get; set; }
    public DateTimeOffset BeginDate { get; set; }
    public Guid? RouteId { get; set; }
    public RouteModel? Route { get; set; }
    public Guid VehicleId { get; set; }
    public VehicleItem? Vehicle { get; set; }
    public ICollection<WaybillFuelModel> Fuels { get; set; }
    public ICollection<WaybillTaskModel> Tasks { get; set; }
    public ICollection<WaybillDetailModel> Details { get; set; }
    public ICollection<EmployeeModel> Drivers { get; set; }
}
