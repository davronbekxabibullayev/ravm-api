namespace Ravm.Application.UseCases.Specializations.Commands;

using Microsoft.EntityFrameworkCore;

public record DeleteSpecializationCommand(Guid Id) : IRequest;

internal class DeleteSpecializationCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteSpecializationCommand>
{
    private readonly IAppDbContext _dbContext = dbContext;
    public async Task Handle(DeleteSpecializationCommand request, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Specializations
            .Where(x => x.Id == request.Id)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.IsDeleted, true), cancellationToken);

        if (result == 0)
        {
            throw new NotFoundException(nameof(Specialization), request.Id);
        }
    }
}
