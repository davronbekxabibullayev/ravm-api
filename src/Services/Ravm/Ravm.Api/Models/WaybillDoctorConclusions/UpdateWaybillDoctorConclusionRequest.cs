namespace Ravm.Api.Models.WaybillDoctorConclusions;

public class UpdateWaybillDoctorConclusionRequest
{
    public Guid WaybillDetailId { get; set; }
    public Guid WaybillDriverId { get; set; }
    public string? Pressure { get; set; }
    public string? Pulse { get; set; }
    public string? Temperature { get; set; }
    public string? Note { get; set; }
    public bool Permitted { get; set; }
}
