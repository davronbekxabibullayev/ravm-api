namespace Ravm.Application.UseCases.Specializations.Queries;

using Microsoft.EntityFrameworkCore;
using Ravm.Application.UseCases.Specializations.Models;

public record GetSpecializationQuery(Guid id) : IRequest<SpecializationModel>;

internal sealed class GetSpecializationQueryHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<GetSpecializationQuery, SpecializationModel>
{
    private readonly IAppDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<SpecializationModel> Handle(GetSpecializationQuery request, CancellationToken cancellationToken)
    {
        var specialization = await _dbContext.Specializations.FirstOrDefaultAsync(x => x.Id == request.id, cancellationToken)
            ?? throw new NotFoundException(nameof(Specialization), request.id);

        return _mapper.Map<SpecializationModel>(specialization);
    }
}
