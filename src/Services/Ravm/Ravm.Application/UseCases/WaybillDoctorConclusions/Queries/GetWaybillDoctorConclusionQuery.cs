namespace Ravm.Application.UseCases.WaybillDoctorConclusions.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.UseCases.WaybillDoctorConclusions.Models;

public record GetWaybillDoctorConclusionQuery(Guid Id) : IRequest<WaybillDoctorConclusionModel>;

public class GetWaybillDoctorConclusionQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetWaybillDoctorConclusionQuery, WaybillDoctorConclusionModel>
{
    public async Task<WaybillDoctorConclusionModel> Handle(GetWaybillDoctorConclusionQuery request,
        CancellationToken cancellationToken)
    {
        var waybillDoctorConclusion = await dbContext.WaybillDoctorConclusions
            .Include(x => x.WaybillDetail)
            .Include(a => a.WaybillDriver!.Employee)
            .FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken)
            ?? throw new NotFoundException(nameof(WaybillDoctorConclusion), request.Id);

        return mapper.Map<WaybillDoctorConclusionModel>(waybillDoctorConclusion);
    }
}
