namespace Ravm.Application.UseCases.Employees.Commands;

using Ravm.Domain.Enums;
using Ravm.Domain.Models;

public record CreateEmployeeCommand(
    string FirstName,
    string LastName,
    string? MiddleName,
    Gender Gender,
    DateTimeOffset? BirthDate,
    Guid OrganizationId,
    OccupationGroupType OccupationGroupType,
    string StaffNumber,
    string? DriverLisenceNumber,
    string? Pin) : IRequest
{
    public ICollection<Guid> Occupations { get; set; } = Array.Empty<Guid>();
    public ICollection<Guid> Specializations { get; set; } = Array.Empty<Guid>();
}

internal class CreateEmployeeCommandHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<CreateEmployeeCommand>
{
    public async Task Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
    {
        var employee = mapper.Map<Employee>(command);

        await dbContext.Employees.AddAsync(employee, cancellationToken);

        //adding occupations
        foreach (var occupation in command.Occupations)
        {
            employee.EmployeeOccupations.Add(new EmployeeOccupation
            {
                OccupationId = occupation
            });
        }

        //adding specializations
        foreach (var specialization in command.Specializations)
        {
            employee.EmployeeSpecializations.Add(new EmployeeSpecialization
            {
                SpecializationId = specialization
            });
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
