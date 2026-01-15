namespace Ravm.Application.UseCases.Employees.Commands;

using Microsoft.EntityFrameworkCore;

public record DeleteEmployeeCommand(Guid Id) : IRequest;

internal sealed class DeleteEmployeeCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteEmployeeCommand>
{
    public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await dbContext.Employees
            .Where(x => x.Id == request.Id)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.IsDeleted, true), cancellationToken);
        if (employee == 0)
            throw new NotFoundException(nameof(Employee), request.Id);
    }
}
