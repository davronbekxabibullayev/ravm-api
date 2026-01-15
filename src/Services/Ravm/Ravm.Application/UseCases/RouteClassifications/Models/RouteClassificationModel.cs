namespace Ravm.Application.UseCases.RouteClassifications.Models;

using Ravm.Application.Common.Models;

public class RouteClassificationModel : LocalizableName
{
    public Guid Id { get; set; }
    public required string Code { get; set; }
}
