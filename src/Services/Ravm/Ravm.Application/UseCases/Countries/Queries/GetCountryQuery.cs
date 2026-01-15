namespace Ravm.Application.UseCases.Countries.Queries;

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ravm.Application.Common;
using Ravm.Application.UseCases.Countries.Models;
using Ravm.Domain.Exceptions;
using Ravm.Domain.Models;

public record GetCountryQuery(Guid Id) : IRequest<CountryModel>;

internal class GetCountryQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetCountryQuery, CountryModel>
{
    public async Task<CountryModel> Handle(GetCountryQuery request, CancellationToken cancellationToken)
    {
        var country = await dbContext.Countries
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Country), request.Id);

        return mapper.Map<CountryModel>(country);
    }
}
