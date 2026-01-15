namespace Ravm.Api.Models.WaybillFuels;

using Ravm.Domain.Enums;

public class UpdateWaybillFuelRequest
{
    public FundingSource FundingSource { get; set; }
    public required string RefuellerFullName { get; set; }
    public DateTimeOffset RefuelDate { get; set; }
    public required string FuelMark { get; set; }
    public FuelType FuelType { get; set; }
    public double Amount { get; set; }
    public double Price { get; set; }
    public Guid WaybillId { get; set; }
    public Guid WaybillDetailId { get; set; }
}
