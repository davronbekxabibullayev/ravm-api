namespace Ravm.Application.UseCases.Employees.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.UseCases.Employees.Models;
using Ravm.Domain.Models;

public record GetEmployeeQuery(Guid Id) : IRequest<EmployeeModel>;

internal sealed class GetEmployeeQueryHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<GetEmployeeQuery, EmployeeModel>
{
    public async Task<EmployeeModel> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        var employee = await dbContext.Employees
            .Include(x => x.EmployeeOccupations)
             .ThenInclude(x => x.Occupation)
            .Include(x => x.EmployeeSpecializations)
             .ThenInclude(x => x.Specialization)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Employee), request.Id);

        return mapper.Map<EmployeeModel>(employee);
    }
}
