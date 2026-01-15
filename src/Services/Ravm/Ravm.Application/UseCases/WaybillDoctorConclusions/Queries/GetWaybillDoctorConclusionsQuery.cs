namespace Ravm.Application.UseCases.WaybillDoctorConclusions.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.UseCases.WaybillDoctorConclusions.Models;

public record GetWaybillDoctorConclusionsQuery(Guid? DoctorId)
    : FilteringRequest, IRequest<PagedList<WaybillDoctorConclusionModel>>;

internal sealed class GetWaybillDoctorConclusionsQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetWaybillDoctorConclusionsQuery, PagedList<WaybillDoctorConclusionModel>>
{
    public async Task<PagedList<WaybillDoctorConclusionModel>> Handle(GetWaybillDoctorConclusionsQuery request,
        CancellationToken cancellationToken)
    {
        var query = dbContext.WaybillDoctorConclusions.AsQueryable();

        if (request.DoctorId.HasValue)
        {
            query = query.Where(x => x.DoctorId == request.DoctorId);
        }

        return await query
            .Include(a => a.WaybillDriver!.Employee)
            .ProjectTo<WaybillDoctorConclusionModel>(mapper.ConfigurationProvider)
            .ToPagedListAsync(request);
    }
}
