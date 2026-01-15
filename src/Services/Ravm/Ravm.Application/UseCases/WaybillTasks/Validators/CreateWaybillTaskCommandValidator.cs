namespace Ravm.Application.UseCases.WaybillTasks.Validators;

using Ravm.Application.UseCases.WaybillTasks.Commands;

public class CreateWaybillTaskCommandValidator : AbstractValidator<CreateWaybillTaskCommand>
{
    public CreateWaybillTaskCommandValidator()
    {
        RuleFor(x => x.TripsAmount).NotEmpty();
        RuleFor(x => x.Distance).NotEmpty();
        RuleFor(x => x.WaybillId).NotEmpty();
    }
}
