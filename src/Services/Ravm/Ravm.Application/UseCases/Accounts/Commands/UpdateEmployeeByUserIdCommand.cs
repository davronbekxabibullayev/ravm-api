namespace Ravm.Application.UseCases.Accounts.Commands;

using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ravm.Domain.Enums;

public record UpdateEmployeeByUserIdCommand(
    Guid UserId,
    string FirstName,
    string LastName,
    string? MiddleName,
    Gender Gender,
    string? Pin,
    DateTimeOffset? BirthDate) : IRequest;

internal class UpdateEmployeeByUserIdCommandHandler(IAppDbContext dbContext) : IRequestHandler<UpdateEmployeeByUserIdCommand>
{
    public async Task Handle(UpdateEmployeeByUserIdCommand request, CancellationToken cancellationToken)
    {
        var employee = await dbContext.Employees.AsTracking().FirstOrDefaultAsync(employee => employee.UserId
                       .Equals(request.UserId), cancellationToken)
                       ?? throw new NotFoundException(nameof(Employee), request.UserId);

        MappingEmployee(request, employee);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static void MappingEmployee(UpdateEmployeeByUserIdCommand request, Employee employee)
    {
        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.MiddleName = request.MiddleName;
        employee.Gender = request.Gender;
        employee.BirthDate = request.BirthDate;
        employee.Pin = request.Pin;
    }
}
