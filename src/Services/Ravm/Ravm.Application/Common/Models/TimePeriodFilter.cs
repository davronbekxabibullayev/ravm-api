namespace Ravm.Application.Common.Models;
using System;

public record TimePeriodFilter
{
    private DateTime _from;
    private DateTime _to;

    public DateTime From { get => _from; set => _from = new DateTime(value.Year, value.Month, value.Day, 0, 0, 0, DateTimeKind.Utc); }
    public DateTime To { get => _to; set => _to = new DateTime(value.Year, value.Month, value.Day, 23, 59, 59, DateTimeKind.Utc); }
    public DateTime PrevFrom => _from.Subtract(_to - _from);
    public DateTime PrevTo => _to.Subtract(_to - _from).AddDays(1).AddTicks(-1);
}
