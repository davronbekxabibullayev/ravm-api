namespace Ravm.Application.UseCases.Routes.Commands;

using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ravm.Application.Common;

public record DeleteRouteCommand(Guid Id) : IRequest;

public class DeleteRouteCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteRouteCommand>
{
    public async Task Handle(DeleteRouteCommand request, CancellationToken cancellationToken)
    {
        var route = await dbContext.Routes
            .Where(w => w.Id == request.Id)
            .ExecuteUpdateAsync(a => a.SetProperty(x => x.IsDeleted, true), cancellationToken);

        if (route == 0)
        {
            throw new NotFoundException(nameof(Route), request.Id);
        }
    }
}
