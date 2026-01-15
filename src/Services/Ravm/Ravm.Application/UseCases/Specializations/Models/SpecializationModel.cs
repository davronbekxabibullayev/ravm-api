namespace Ravm.Application.UseCases.Specializations.Models;

using Ravm.Application.Common.Models;

public class SpecializationModel : LocalizableName
{
    public required Guid Id { get; set; }

    public required string Code { get; set; }

}
