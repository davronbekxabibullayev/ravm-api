namespace Ravm.Application.UseCases.Accounts.Commands;

using MediatR;
using Microsoft.EntityFrameworkCore;

public record DeleteEmployeeAccountCommand(Guid Id) : IRequest;

internal sealed class DeleteEmployeeAccountCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteEmployeeAccountCommand>
{
    public async Task Handle(DeleteEmployeeAccountCommand command, CancellationToken cancellationToken)
    {
        var deletedRows = await dbContext.Users
            .Where(x => x.Id == command.Id)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.IsDeleted, true), cancellationToken);

        if (deletedRows == 0)
        {
            throw new NotFoundException(nameof(User), command.Id);
        }
    }
}
