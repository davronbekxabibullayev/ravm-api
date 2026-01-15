namespace Ravm.Application.UseCases.Countries.Models;

using Ravm.Application.Common.Models;

public class CountryModel : LocalizableName
{
    public Guid Id { get; set; }
    public required string Code { get; set; }
    public string? StateCode { get; set; }
}
