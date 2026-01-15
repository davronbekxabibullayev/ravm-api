namespace Ravm.Application.UseCases.StopPoints.Models;

using System;
using Ravm.Domain.Enums;

public class StopPointModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string NameRu { get; set; }
    public string? NameUz { get; set; }
    public string? NameKa { get; set; }
    public required string Code { get; set; }
    public StopPointPosition Position { get; set; }
}
