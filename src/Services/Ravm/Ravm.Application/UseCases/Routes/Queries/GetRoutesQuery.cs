namespace Ravm.Application.UseCases.Routes.Queries;

using System.Threading;
using System.Threading.Tasks;
using Ravm.Application.Extensions;
using Ravm.Application.UseCases.Routes.Models;

public record GetRoutesQuery(Guid? OrganizationId) : FilteringRequest, IRequest<PagedList<RouteModel>>;

public class GetRoutesQueryHandler(IAppDbContext appDbContext,
    IMapper mapper, ICurrentUser currentUser) : IRequestHandler<GetRoutesQuery, PagedList<RouteModel>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<PagedList<RouteModel>> Handle(GetRoutesQuery request, CancellationToken cancellationToken)
    {
        var routes = _appDbContext.Routes
                                  .IncludeChilds(currentUser.OrganizationId)
                                  .AsQueryable();

        if (request.OrganizationId.HasValue)
        {
            routes = routes.Where(x => x.OrganizationId.Equals(request.OrganizationId));
        }

        return await routes.ToPagedListAsync<Route, RouteModel>(request, _mapper);
    }
}
