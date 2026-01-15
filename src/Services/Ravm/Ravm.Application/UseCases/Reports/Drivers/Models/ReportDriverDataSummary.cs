namespace Ravm.Application.UseCases.Reports.Drivers.Models;

using Ravm.Application.UseCases.Employees.Models;

public class ReportDriverDataSummary
{
    public int RouteCount { get; set; }
    public int TaskCount { get; set; }
    public required EmployeeModel Driver { get; set; }
    public TimeSpan WorkedTimeTotalOnTask { get; set; }
    public TimeSpan WorkedTimeTotalOnRoute { get; set; }
    public double TraveledDistanceTotalOnTask { get; set; }
    public double TraveledDistanceTotalOnRoute { get; set; }
}
