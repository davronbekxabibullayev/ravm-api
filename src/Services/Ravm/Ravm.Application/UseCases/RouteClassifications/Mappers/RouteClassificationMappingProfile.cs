namespace Ravm.Application.UseCases.RouteClassifications.Mappers;

using Ravm.Application.UseCases.RouteClassifications.Commands;
using Ravm.Application.UseCases.RouteClassifications.Models;

internal class RouteClassificationMappingProfile : Profile
{
    public RouteClassificationMappingProfile()
    {
        CreateMap<RouteClassification, RouteClassificationModel>();
        CreateMap<UpdateRouteClassificationCommand, RouteClassification>();
        CreateMap<CreateRouteClassificationCommand, RouteClassification>();
    }
}
