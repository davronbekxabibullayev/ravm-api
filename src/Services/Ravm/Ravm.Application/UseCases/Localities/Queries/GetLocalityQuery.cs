namespace Ravm.Application.UseCases.Localities.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.UseCases.Localities.Models;

public record GetLocalityQuery(Guid Id) : IRequest<LocalityModel>;
internal sealed class GetLocalityQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetLocalityQuery, LocalityModel>
{
    private readonly IAppDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<LocalityModel> Handle(GetLocalityQuery request, CancellationToken cancellationToken)
    {
        var locality = await _dbContext.Localities.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Locality), request.Id);

        return _mapper.Map<LocalityModel>(locality);
    }
}
