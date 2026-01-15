namespace Ravm.Application.UseCases.Employees.Commands;

using Microsoft.EntityFrameworkCore;
using Ravm.Domain.Enums;
using Ravm.Domain.Models;

public record UpdateEmployeeCommand(
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
    public Guid Id { get; set; }
    public ICollection<Guid> Occupations { get; set; } = Array.Empty<Guid>();
    public ICollection<Guid> Specializations { get; set; } = Array.Empty<Guid>();
}

internal sealed class UpdateEmployeeCommandHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateEmployeeCommand>
{
    public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await GetEmployee(request)
            ?? throw new NotFoundException(nameof(Employee), request.Id);

        mapper.Map(request, employee);
        UpdateOccupations(request, employee);
        UpdateSpecializations(request, employee);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task<Employee?> GetEmployee(UpdateEmployeeCommand request)
    {
        return await dbContext.Employees
            .AsTracking()
            .Include(x => x.EmployeeOccupations)
            .Include(x => x.EmployeeSpecializations)
            .FirstOrDefaultAsync(x => x.Id == request.Id);
    }

    private static void UpdateOccupations(UpdateEmployeeCommand request, Employee employee)
    {
        foreach (var occupation in employee.EmployeeOccupations)
        {
            employee.EmployeeOccupations.Remove(occupation);
        }
        foreach (var newOccupation in request.Occupations)
        {
            employee.EmployeeOccupations.Add(new EmployeeOccupation
            {
                OccupationId = newOccupation
            });
        }
    }

    private static void UpdateSpecializations(UpdateEmployeeCommand request, Employee employee)
    {
        foreach (var specialization in employee.EmployeeSpecializations)
        {
            employee.EmployeeSpecializations.Remove(specialization);
        }
        foreach (var newSpecialization in request.Specializations)
        {
            employee.EmployeeSpecializations.Add(new EmployeeSpecialization
            {
                SpecializationId = newSpecialization
            });
        }
    }
}
