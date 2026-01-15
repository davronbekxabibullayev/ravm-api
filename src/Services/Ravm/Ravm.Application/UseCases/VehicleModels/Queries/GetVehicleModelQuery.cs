namespace Ravm.Application.UseCases.VehicleModels.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.UseCases.VehicleModels.Models;

public record GetVehicleModelQuery(Guid Id) : IRequest<VehicleModelModel>;

internal class GetVehicleModelQueryHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<GetVehicleModelQuery, VehicleModelModel>
{
    public async Task<VehicleModelModel> Handle(GetVehicleModelQuery request, CancellationToken cancellationToken)
    {
        var vehicleModel = await dbContext.VehicleModels
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(VehicleModel), request.Id);

        return mapper.Map<VehicleModelModel>(vehicleModel);
    }
}
