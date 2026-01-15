namespace Ravm.Application.UseCases.Countries.Queries;

using Ravm.Application.UseCases.Countries.Models;

public record GetCountriesQuery : FilteringRequest, IRequest<PagedList<CountryModel>>;

internal sealed class GetCountriesQueryHandler(
    IAppDbContext dbContext,
    IMapper mapper)
    : IRequestHandler<GetCountriesQuery, PagedList<CountryModel>>
{
    public async Task<PagedList<CountryModel>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Countries
            .AsQueryable()
            .ProjectTo<CountryModel>(mapper.ConfigurationProvider)
            .ToPagedListAsync(request);
    }
}
