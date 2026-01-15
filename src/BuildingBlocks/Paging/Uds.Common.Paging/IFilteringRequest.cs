namespace Devhub.Common.Paging;

public interface IFilteringRequest : IPagingRequest
{
    string? Field { get; set; }

    string? Operator { get; set; }

    string? Value { get; set; }

    string? Logic { get; set; }

    IFilteringRequest[]? Filters { get; set; }

    IList<IFilteringRequest> All();

    void Collect(IList<IFilteringRequest> filters);

    bool Validate();

    bool ValidateAndThrow();
}
