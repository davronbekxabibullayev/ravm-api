namespace Ravm.Application.UseCases.Specializations.Commands;

using Microsoft.EntityFrameworkCore;

public record UpdateSpecializationCommand(
   Guid Id,
   string Name,
   string NameRu,
   string? NameUz,
   string? NameKa,
   string Code) : IRequest;


internal class UpdateSpecializationCommandHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateSpecializationCommand>
{
    private readonly IAppDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task Handle(UpdateSpecializationCommand request, CancellationToken cancellationToken)
    {
        var specialization = await GetSpecializationsAsync(request.Id)
            ?? throw new NotFoundException(nameof(Specialization), request.Id);

        _mapper.Map(request, specialization);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private Task<Specialization?> GetSpecializationsAsync(Guid Id)
    {
        return _dbContext.Specializations
            .AsTracking()
            .FirstOrDefaultAsync(x => x.Id == Id);
    }

}
