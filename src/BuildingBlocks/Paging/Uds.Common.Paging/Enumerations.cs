namespace Devhub.Common.Paging;
public class SortingType
{
    public const string OrderByAsc = "asc";
    public const string OrderByDesc = "desc";
}

public enum OperatorType
{
    And = 1,
    Or = 2,
    None = 3
}

public static class OperatorTypes
{
    private const string ConstantAnd = "and";
    private const string ConstantOr = "or";

    public static OperatorType ConvertToOperatorType(string? value)
    {
        return value?.ToLowerInvariant() switch
        {
            ConstantAnd => OperatorType.And,
            ConstantOr => OperatorType.Or,
            _ => OperatorType.None,
        };
    }
}
