namespace Ravm.Api.Models.Reports;

public record GetReportsByPeriodRequest(
    DateTime From,
    DateTime To);
