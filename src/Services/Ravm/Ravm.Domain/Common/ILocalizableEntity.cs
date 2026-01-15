namespace Ravm.Domain.Common;

public interface ILocalizableEntity
{
    string Name { get; set; }
    string NameRu { get; set; }
    string? NameUz { get; set; }
    string? NameKa { get; set; }
}
