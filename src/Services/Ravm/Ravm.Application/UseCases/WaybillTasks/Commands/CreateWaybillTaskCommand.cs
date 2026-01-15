namespace Ravm.Application.UseCases.WaybillTasks.Commands;

public record CreateWaybillTaskCommand : IRequest
{
    public required string Number { get; set; }
    public string? Customer { get; set; }
    public string? CargoInfo { get; set; }
    public string? Note { get; set; }
    public int TripsAmount { get; set; }
    public DateTimeOffset Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public double Distance { get; set; }
    public string? AddressTo { get; set; }
    public string? AddressFrom { get; set; }
    public Guid WaybillId { get; set; }
}

internal class CreateWaybillTaskCommandHandler(IAppDbContext dbContext) : IRequestHandler<CreateWaybillTaskCommand>
{
    public async Task Handle(CreateWaybillTaskCommand request, CancellationToken cancellationToken)
    {
        var waybillTask = NewWaybillTask(request);

        await dbContext.WaybillTasks.AddAsync(waybillTask, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static WaybillTask NewWaybillTask(CreateWaybillTaskCommand request)
    {
        return new WaybillTask
        {
            Number = request.Number,
            Customer = request.Customer,
            CargoInfo = request.CargoInfo,
            Note = request.Note,
            TripsAmount = request.TripsAmount,
            Date = request.Date,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Distance = request.Distance,
            AddressTo = request.AddressTo,
            AddressFrom = request.AddressFrom,
            WaybillId = request.WaybillId
        };
    }
}
