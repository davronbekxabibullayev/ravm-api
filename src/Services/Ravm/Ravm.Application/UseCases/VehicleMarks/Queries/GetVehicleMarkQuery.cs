namespace Ravm.Application.UseCases.VehicleMarks.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.UseCases.VehicleMarks.Models;

public record GetVehicleMarkQuery(Guid Id) : IRequest<VehicleMarkModel>;

internal class GetVehicleMarkQueryHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<GetVehicleMarkQuery, VehicleMarkModel>
{
    public async Task<VehicleMarkModel> Handle(GetVehicleMarkQuery request, CancellationToken cancellationToken)
    {
        var vehicleMark = await dbContext.VehicleMarks
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(VehicleMark), request.Id);

        return mapper.Map<VehicleMarkModel>(vehicleMark);
    }
}
