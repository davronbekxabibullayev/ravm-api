namespace Ravm.Api.Models.Waybills;

public class UpdateWaybillRequest
{
    public required string Number { get; set; }
    public Guid OrganizationId { get; set; }
    public DateTimeOffset ExpireDate { get; set; }
    public DateTimeOffset BeginDate { get; set; }
    public Guid? RouteId { get; set; }
    public Guid VehicleId { get; set; }
    public ICollection<Guid>? DriverIds { get; set; }
}
