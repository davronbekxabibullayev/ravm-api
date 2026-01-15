namespace Ravm.Application.UseCases.Routes.Mappers;

using Ravm.Application.UseCases.Routes.Commands;
using Ravm.Application.UseCases.Routes.Models;

public class RouteProfile : Profile
{
    public RouteProfile()
    {
        CreateMap<CreateRouteCommand, Route>();
        CreateMap<UpdateRouteCommand, Route>();
        CreateMap<Route, RouteWithDetailsModel>()
            .ForMember(x => x.StopPoints, act => act.MapFrom(a => a.RouteStopPoints.Select(s => s.StopPoint)));
        CreateMap<Route, RouteModel>();
    }
}
