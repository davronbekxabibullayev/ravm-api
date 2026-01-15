namespace Ravm.Application.UseCases.Vehicles.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.Extensions;
using Ravm.Application.UseCases.Vehicles.Models;

public record GetVehiclesQuery(Guid? VehicleModelId, string? GarageNumber, string? StateNumber) : FilteringRequest, IRequest<PagedList<VehicleItemDto>>
{
    public Guid? OrganizationId { get; set; }
}

internal sealed class GetVehiclesQueryHandler(
    IAppDbContext dbContext,
    IMapper mapper,
    ICurrentUser currentUser) : IRequestHandler<GetVehiclesQuery, PagedList<VehicleItemDto>>
{
    public async Task<PagedList<VehicleItemDto>> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
    {
        var vehicles = dbContext.Vehicles
                                .IncludeChilds(currentUser.OrganizationId)
                                .Include(x => x.Organization)
                                .Include(x => x.VehicleModel!.VehicleMark)
                                .Where(x => x.OrganizationId == request.OrganizationId)
                                .AsQueryable();

        if (request.OrganizationId.HasValue)
        {
            vehicles = vehicles.Where(x => x.OrganizationId == request.OrganizationId.Value);
        }

        vehicles = (from vehicle in vehicles
                    where (!request.VehicleModelId.HasValue || vehicle.VehicleModelId == request.VehicleModelId)
                        && (string.IsNullOrEmpty(request.GarageNumber) || vehicle.GarageNumber == request.GarageNumber)
                        && (string.IsNullOrEmpty(request.StateNumber) || vehicle.StateNumber == request.StateNumber)
                    select vehicle);

        return await vehicles.ToPagedListAsync<Vehicle, VehicleItemDto>(request, mapper);
    }
}
