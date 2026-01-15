namespace Ravm.Api.Models.WaybillTasks;

public class UpdateWaybillTaskRequest
{
    public required string Number { get; set; }
    public string? Customer { get; set; }
    public string? CargoInfo { get; set; }
    public string? Note { get; set; }
    public int TripsAmount { get; set; }
    public DateTimeOffset Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public double Distance { get; set; }
    public string? AddressTo { get; set; }
    public string? AddressFrom { get; set; }
    public Guid WaybillId { get; set; }
}
