namespace Ravm.Application.UseCases.Regions.Models;

using Ravm.Application.Common.Models;

public class RegionModel : LocalizableName
{
    public required Guid Id { get; set; }

    public Guid CountryId { get; set; }

    public required string Code { get; set; }

    public string? StateCode { get; set; }

}
