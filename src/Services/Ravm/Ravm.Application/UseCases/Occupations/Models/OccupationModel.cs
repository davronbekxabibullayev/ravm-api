namespace Ravm.Application.UseCases.Occupations.Models;

using Ravm.Application.Common.Models;

public class OccupationModel : LocalizableName
{
    public required Guid Id { get; set; }
    public required string Code { get; set; }
}
