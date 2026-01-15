namespace Ravm.Domain.Common;

public interface IDeletable
{
    bool IsDeleted { get; set; }
}