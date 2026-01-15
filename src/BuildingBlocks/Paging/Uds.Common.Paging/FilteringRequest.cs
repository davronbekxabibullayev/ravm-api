namespace Devhub.Common.Paging;

using System.Runtime.Serialization;

/// <summary>
/// Represents a filter expression of Kendo DataSource.
/// </summary>
[DataContract]
public record FilteringRequest : PagingRequest, IFilteringRequest
{
    /// <summary>
    /// Gets or sets the name of the sorted field (property). Set to <c>null</c> if the <c>Filters</c> property is set.
    /// </summary>
    [DataMember(Name = "field")]
    public string? Field { get; set; }

    /// <summary>
    /// Gets or sets the filtering operator. Set to <c>null</c> if the <c>Filters</c> property is set.
    /// </summary>
    [DataMember(Name = "operator")]
    public string? Operator { get; set; }

    /// <summary>
    /// Gets or sets the filtering value. Set to <c>null</c> if the <c>Filters</c> property is set.
    /// </summary>
    [DataMember(Name = "value")]
    public string? Value { get; set; }

    /// <summary>
    /// Gets or sets the filtering logic. Can be set to "or" or "and". Set to <c>null</c> unless <c>Filters</c> is set.
    /// </summary>
    [DataMember(Name = "logic")]
    public string? Logic { get; set; }

    /// <summary>
    /// Gets or sets the child filter expressions. Set to <c>null</c> if there are no child expressions.
    /// </summary>
    [DataMember(Name = "filters")]
    public IFilteringRequest[]? Filters { get; set; }


    /// <summary>
    /// Get a flattened list of all child filter expressions.
    /// </summary>
    public IList<IFilteringRequest> All()
    {
        var filters = new List<IFilteringRequest>();

        Collect(filters);

        return filters;
    }

    public void Collect(IList<IFilteringRequest> filters)
    {
        if (Filters != null && Filters.Length > 0)
        {
            foreach (var filter in Filters)
            {
                filters.Add(filter);

                filter.Collect(filters);
            }
        }
        else
        {
            filters.Add(this);
        }
    }

    public bool Validate()
    {
        return !(string.IsNullOrWhiteSpace(Field) || Value == null) || Filters?.All(a => a.Validate()) == true;
    }

    public bool ValidateAndThrow()
    {
        if (!Validate())
            throw new InvalidOperationException("Filter model is not valid.");

        return true;
    }
}
