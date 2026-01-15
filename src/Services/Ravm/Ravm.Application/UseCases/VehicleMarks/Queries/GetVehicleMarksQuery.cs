namespace Ravm.Application.UseCases.VehicleMarks.Queries;

using Ravm.Application.UseCases.VehicleMarks.Models;

public record GetVehicleMarksQuery() : FilteringRequest, IRequest<PagedList<VehicleMarkModel>>;

internal sealed class GetVehicleMarksQueryHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<GetVehicleMarksQuery, PagedList<VehicleMarkModel>>
{
    public async Task<PagedList<VehicleMarkModel>> Handle(GetVehicleMarksQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.VehicleMarks
            .AsQueryable()
            .ProjectTo<VehicleMarkModel>(mapper.ConfigurationProvider)
            .ToPagedListAsync(request);
    }
}

