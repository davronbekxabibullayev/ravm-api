namespace Ravm.Application.UseCases.Reasons.Models;

using Ravm.Application.Common.Models;

public class ReasonModel : LocalizableName
{
    public Guid Id { get; set; }

    public required string Code { get; set; }
}
