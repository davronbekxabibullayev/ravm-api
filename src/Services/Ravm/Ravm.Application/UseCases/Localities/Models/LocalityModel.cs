
namespace Ravm.Application.UseCases.Localities.Models;

using Ravm.Application.Common.Models;

public class LocalityModel : LocalizableName
{
    public required Guid Id { get; set; }
    public Guid? RegionId { get; set; }
    public Guid? CityId { get; set; }
    public required string Code { get; set; }
    public string? StateCode { get; set; }
}
