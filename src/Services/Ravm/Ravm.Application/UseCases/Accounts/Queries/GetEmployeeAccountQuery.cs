namespace Ravm.Application.UseCases.Accounts.Queries;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ravm.Application.UseCases.Accounts.Models;
using Ravm.Domain.Models;

public record GetEmployeeAccountQuery(Guid EmployeeId) : IRequest<AccountModel>;

internal sealed class GetEmployeeAccountQueryHandler(IMapper mapper, UserManager<User> userManager) : IRequestHandler<GetEmployeeAccountQuery, AccountModel>
{
    public async Task<AccountModel> Handle(GetEmployeeAccountQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(a => a.EmployeeId == request.EmployeeId, cancellationToken)
            ?? throw new NotFoundException(nameof(User), nameof(request.EmployeeId), request.EmployeeId);

        var roles = await userManager.GetRolesAsync(user);

        var result = mapper.Map<AccountModel>(user);

        result.Roles = roles;

        return result;
    }
}
