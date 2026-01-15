namespace Ravm.Application.UseCases.Employees.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.Common;
using Ravm.Application.Extensions;
using Ravm.Application.UseCases.Employees.Models;

public record GetEmployeesQuery(string? Name, Guid? OrganizationId) : FilteringRequest, IRequest<PagedList<EmployeeModel>>;

internal sealed class GetEmployeesQueryHandler(IAppDbContext dbContext, IMapper mapper, ICurrentUser currentUser) : IRequestHandler<GetEmployeesQuery, PagedList<EmployeeModel>>
{
    public async Task<PagedList<EmployeeModel>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        var orgId = currentUser.OrganizationId;

        var employees = dbContext.Employees
            .IncludeChilds(currentUser.OrganizationId)
            .Include(x => x.EmployeeOccupations)
             .ThenInclude(x => x.Occupation)
            .Include(x => x.EmployeeSpecializations)
             .ThenInclude(x => x.Specialization)
            .AsQueryable();

        if (request.OrganizationId.HasValue)
        {
            employees = employees.Where(x => x.OrganizationId.Equals(request.OrganizationId));
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            var employeeName = request.Name.ToLowerInvariant().Trim();

            employees = employees.Where(x => EF.Functions.ILike(x.FullName, $"%{employeeName}%"));
        }

        var result = await employees.ToPagedListAsync<Employee, EmployeeModel>(request, mapper);

        return result;
    }
}
