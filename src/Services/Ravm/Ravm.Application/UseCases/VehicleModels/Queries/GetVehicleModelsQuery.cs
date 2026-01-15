namespace Ravm.Application.UseCases.VehicleModels.Queries;

using Ravm.Application.UseCases.VehicleModels.Models;

public record GetVehicleModelsQuery(Guid? VehicleMarkId) : FilteringRequest, IRequest<PagedList<VehicleModelModel>>;

internal sealed class GetVehicleModelsQueryHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<GetVehicleModelsQuery, PagedList<VehicleModelModel>>
{
    public async Task<PagedList<VehicleModelModel>> Handle(GetVehicleModelsQuery request, CancellationToken cancellationToken)
    {
        var models = dbContext.VehicleModels.AsQueryable();

        if (request.VehicleMarkId.HasValue)
            models = models.Where(x => x.VehicleMarkId == request.VehicleMarkId);

        return await models.ProjectTo<VehicleModelModel>(mapper.ConfigurationProvider).ToPagedListAsync(request);
    }
}

