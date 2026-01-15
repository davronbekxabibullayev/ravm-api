namespace Ravm.Application.UseCases.WaybillFuels.Commands;

using Ravm.Domain.Enums;

public record CreateWaybillFuelCommand : IRequest
{
    public FundingSource FundingSource { get; set; }
    public required string RefuellerFullName { get; set; }
    public DateTimeOffset RefuelDate { get; set; }
    public required string FuelMark { get; set; }
    public FuelType FuelType { get; set; }
    public double Amount { get; set; }
    public double Price { get; set; }
    public Guid WaybillId { get; set; }
    public Guid WaybillDetailId { get; set; }
}

internal class CreateWaybillFuelCommandHandler(IAppDbContext dbContext) : IRequestHandler<CreateWaybillFuelCommand>
{
    public async Task Handle(CreateWaybillFuelCommand request, CancellationToken cancellationToken)
    {
        var waybillFuel = NewWaybillFuel(request);

        await dbContext.WaybillFuels.AddAsync(waybillFuel, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static WaybillFuel NewWaybillFuel(CreateWaybillFuelCommand request)
    {
        return new WaybillFuel
        {
            Amount = request.Amount,
            FuelMark = request.FuelMark,
            FuelType = request.FuelType,
            FundingSource = request.FundingSource,
            Price = request.Price,
            RefuellerFullName = request.RefuellerFullName,
            RefuelDate = request.RefuelDate,
            WaybillId = request.WaybillId,
            WaybillDetailId = request.WaybillDetailId
        };
    }
}
