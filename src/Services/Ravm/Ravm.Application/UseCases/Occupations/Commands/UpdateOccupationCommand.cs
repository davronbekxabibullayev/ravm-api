namespace Ravm.Application.UseCases.Occupations.Commands;
using Microsoft.EntityFrameworkCore;

public record UpdateOccupationCommand(
   Guid Id,
   string Name,
   string NameRu,
   string? NameUz,
   string? NameKa,
   string Code) : IRequest;
internal class UpdateOccupationCommandHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateOccupationCommand>
{
    private readonly IAppDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task Handle(UpdateOccupationCommand request, CancellationToken cancellationToken)
    {
        var occupation = await GetOccupationsAsync(request.Id)
            ?? throw new NotFoundException(nameof(Domain.Models.Occupation), request.Id);

        _mapper.Map(request, occupation);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private Task<Domain.Models.Occupation?> GetOccupationsAsync(Guid id)
    {
        return _dbContext.Occupations
                    .AsTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
    }
}
