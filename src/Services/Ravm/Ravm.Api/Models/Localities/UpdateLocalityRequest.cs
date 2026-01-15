namespace Ravm.Api.Models.Localities;

public class UpdateLocalityRequest
{
    public required string Name { get; set; }
    public required string NameRu { get; set; }
    public string? NameUz { get; set; }
    public string? NameKa { get; set; }
    public string? StateCode { get; set; }
    public required string Code { get; set; }
    public required Guid RegionId { get; set; }
    public required Guid CityId { get; set; }
}
