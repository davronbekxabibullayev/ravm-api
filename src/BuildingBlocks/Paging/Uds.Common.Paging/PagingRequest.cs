namespace Devhub.Common.Paging;

using System.ComponentModel.DataAnnotations;

public record PagingRequest : IPagingRequest, ISortingRequest
{
    [Required]
    public int Page { get; set; }

    [Required]
    public int PageSize { get; set; } = 100;

    public string? SortBy { get; set; }
}
