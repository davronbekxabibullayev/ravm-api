namespace Ravm.Api.Models.StopPoints;

using Ravm.Domain.Enums;

public class UpdateStopPointRequest
{
    public required string Name { get; set; }
    public required string NameRu { get; set; }
    public string? NameUz { get; set; }
    public string? NameKa { get; set; }
    public required string Code { get; set; }
    public StopPointPosition Position { get; set; }
}
