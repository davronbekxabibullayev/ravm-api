namespace Ravm.Application.UseCases.Cities.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.UseCases.Cities.Models;

public record GetCityQuery(Guid Id) : IRequest<CityModel>;

internal sealed class GetCityQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetCityQuery, CityModel>
{
    public async Task<CityModel> Handle(GetCityQuery request, CancellationToken cancellationToken)
    {
        var city = await dbContext.Cities
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(City), request.Id);

        return mapper.Map<CityModel>(city);
    }
}
