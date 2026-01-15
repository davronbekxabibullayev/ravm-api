namespace Ravm.Application.UseCases.Countries.Commands;

using Microsoft.EntityFrameworkCore;

public record UpdateCountryCommand(
    Guid Id,
    string Name,
    string NameRu,
    string? NameUz,
    string? NameKa,
    string Code,
    string? StateCode) : IRequest;

internal class UpdateCountryCommandHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateCountryCommand>
{
    public async Task Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
    {
        var country = await GetCountryAsync(request.Id)
            ?? throw new NotFoundException(nameof(Country), request.Id);

        mapper.Map(request, country);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private Task<Country?> GetCountryAsync(Guid id)
    {
        return dbContext.Countries
                    .AsTracking()
                    .FirstOrDefaultAsync(w => w.Id == id);
    }
}
