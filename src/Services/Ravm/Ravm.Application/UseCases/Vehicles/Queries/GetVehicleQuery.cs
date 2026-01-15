namespace Ravm.Application.UseCases.Vehicles.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.UseCases.Vehicles.Models;

public record GetVehicleQuery(Guid Id) : IRequest<VehicleItemDto>;

internal sealed class GetVehicleQueryHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<GetVehicleQuery, VehicleItemDto>
{
    public async Task<VehicleItemDto> Handle(GetVehicleQuery request, CancellationToken cancellationToken)
    {
        var vehicle = await dbContext.Vehicles
            .Include(x => x.Organization)
            .Include(x => x.VehicleModel)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Vehicle), request.Id);

        return mapper.Map<VehicleItemDto>(vehicle);
    }
}
