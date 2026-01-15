namespace Ravm.Application.UseCases.VehicleMarks.Models;

using Ravm.Application.Common.Models;

public class VehicleMarkModel : LocalizableName
{
    public Guid Id { get; set; }

    public required string Code { get; set; }
}
